// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Results;
using HamkareBlazor.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace HamkareBlazor
{
    /// <summary>
    /// Non-generic marker of <see cref="HamkareAutoValidateForm{TModel}"/>.
    /// Lets consumer code (e.g. shared submit base classes) detect forms which already run
    /// model-level validation (FluentValidation + DataAnnotations) on their own.
    /// </summary>
    public abstract class HamkareAutoValidateFormBase : HamkareForm
    {
    }

    /// <summary>
    /// A <see cref="HamkareForm"/> which validates the bound <see cref="HamkareForm.Model"/> automatically:
    /// <para>
    /// 1. <b>FluentValidation</b>: the <see cref="Validator"/> parameter when provided, otherwise every
    /// <c>IValidator&lt;TModel&gt;</c> registered in the DI container (e.g. via
    /// <c>services.AddValidatorsFromAssemblyContaining&lt;T&gt;()</c> from FluentValidation.DependencyInjectionExtensions).
    /// </para>
    /// <para>
    /// 2. <b>DataAnnotations</b> (<c>Validator.TryValidateObject</c>) so legacy attributes like
    /// <see cref="RequiredAttribute"/> and <c>IValidatableObject</c> keep working (configurable).
    /// </para>
    /// <para>
    /// Failures which can be mapped to an input (through its <c>For</c> member path) are displayed under that input
    /// using the native form <c>Validation</c> hook. Failures without a matching field (class-level rules,
    /// properties without an input) are collected in <see cref="HamkareForm.Errors"/> so the page can show them
    /// in the Snackbar.
    /// </para>
    /// <para>
    /// Notes: every input inside this form must set its <c>For</c> parameter (same requirement as MudBlazor's
    /// model-with-path validation). When a field value changes, only that field is re-validated against the
    /// model, using a small cache so one validation burst executes the model validators once.
    /// </para>
    /// </summary>
    /// <typeparam name="TModel">The model type populated by this form.</typeparam>
    public class HamkareAutoValidateForm<TModel> : HamkareAutoValidateFormBase
    {
        private IReadOnlyList<ValidationFailure>? _cachedFailures;
        private TModel? _cachedModel;
        private long _cachedAt;
        private Task<IReadOnlyList<ValidationFailure>>? _inFlight;
        private TModel? _inFlightModel;
        private Func<object, string, Task<IEnumerable<string?>>>? _fieldValidationAdapter;

        [Inject]
        private IServiceProvider ServiceProvider { get; set; } = null!;

        /// <summary>
        /// The strongly typed model populated by this form.
        /// </summary>
        /// <remarks>
        /// Shadows (keeps in sync with) <see cref="HamkareForm.Model"/> so the field-level Validation hook
        /// can read the typed model through <c>IForm.Model</c>.
        /// </remarks>
        [Parameter]
        public new TModel? Model
        {
            get => (TModel?)base.Model;
            set => base.Model = value;
        }

        /// <summary>
        /// An optional, manually provided FluentValidation validator.
        /// </summary>
        /// <remarks>
        /// When set, it wins over DI resolution. When <c>null</c>, all <c>IValidator&lt;TModel&gt;</c>
        /// registrations resolved from the DI container are used (no registration = FluentValidation is skipped silently).
        /// </remarks>
        [Parameter]
        public IValidator<TModel>? Validator { get; set; }

        /// <summary>
        /// Whether FluentValidation validators participate in the automatic model validation.
        /// </summary>
        /// <remarks>Defaults to <c>true</c>.</remarks>
        [Parameter]
        public bool UseFluentValidation { get; set; } = true;

        /// <summary>
        /// Whether DataAnnotations (<c>Validator.TryValidateObject</c>) participate in the automatic model validation.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c> to keep compatibility with models that rely on
        /// <see cref="RequiredAttribute"/>-style attributes or implement <c>IValidatableObject</c>.
        /// </remarks>
        [Parameter]
        public bool UseDataAnnotations { get; set; } = true;

        /// <summary>
        /// Whether <see cref="ValidateAsync"/> also validates the model automatically (submit-time validation).
        /// </summary>
        /// <remarks>Defaults to <c>true</c>. Set to <c>false</c> to keep the plain HamkareForm behavior.</remarks>
        [Parameter]
        public bool AutoModelValidation { get; set; } = true;

        /// <summary>
        /// Whether the model validation failures are wired to the fields via the native form
        /// <c>Validation</c> hook so they are displayed under the matching inputs.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c>. Set to <c>false</c> when the form contains inputs without a
        /// <c>For</c> parameter; then all model failures are reported on the form level only.
        /// </remarks>
        [Parameter]
        public bool AutoFieldValidation { get; set; } = true;

        /// <summary>
        /// Whether <see cref="HamkareForm.Errors"/> also includes the failures which were successfully
        /// mapped to a field.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>: form-level errors then contain only the messages which could NOT be
        /// attached to any field (class-level rules, properties without an input) — ideal for the Snackbar,
        /// while field errors are shown under the inputs. Set to <c>true</c> for the legacy behavior where
        /// the Snackbar repeats every message.
        /// </remarks>
        [Parameter]
        public bool IncludeFieldErrorsInErrors { get; set; } = false;

        /// <summary>
        /// The cache window (in milliseconds) during which repeated validation calls within one burst reuse
        /// the same model validation result.
        /// </summary>
        /// <remarks>Defaults to <c>100</c>. Set to <c>0</c> to disable caching.</remarks>
        [Parameter]
        public int ValidationCacheMilliseconds { get; set; } = 100;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (AutoFieldValidation && (UseFluentValidation || UseDataAnnotations) && Validation is null)
                Validation = FieldValidationAdapter;
        }

        /// <summary>
        /// Validates all fields (native behavior), then runs the model-level validation
        /// (FluentValidation + DataAnnotations) once and merges the failures which could not be mapped to any
        /// field into the form-level <see cref="HamkareForm.Errors"/>.
        /// </summary>
        public override async Task ValidateAsync()
        {
            if (!AutoModelValidation)
            {
                await base.ValidateAsync();
                return;
            }

            // 1) Warm the model validation cache so the per-field Validation hook (invoked by
            //    base.ValidateAsync for every registered input) reuses one single model validation run.
            if (Model is not null)
                await GetModelFailuresTaskAsync(Model);

            // 2) Field validation (native HamkareForm behavior).
            await base.ValidateAsync();

            // 3) Merge failures without a matching field into the form level errors (Snackbar candidates).
            if (Model is null)
                return;

            var failures = await GetModelFailuresTaskAsync(Model);
            var controlPaths = GetControlMemberPaths().ToArray();

            var messages = failures
                .Where(f => IncludeFieldErrorsInErrors || !MatchesAnyControlPath(f, controlPaths))
                .Select(f => f.ErrorMessage)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Select(m => m.Trim())
                .Distinct()
                .ToList();

            if (messages.Count == 0)
                return;

            foreach (var message in messages)
                _errors.Add(message);

            if (IsValid)
            {
                IsValid = false;
                await IsValidChanged.InvokeAsync(false);
            }

            await ErrorsChanged.InvokeAsync(Errors);
        }

        /// <inheritdoc />
        public override async Task ResetAsync()
        {
            InvalidateModelValidationCache();
            await base.ResetAsync();
        }

        /// <inheritdoc />
        public override async Task ResetValidationAsync()
        {
            InvalidateModelValidationCache();
            await base.ResetValidationAsync();
        }

        /// <summary>
        /// The adapter assigned to the native form-level <c>Validation</c> hook. Every field calls it with
        /// (Form.Model, full member path of its For) and receives the failures belonging to that member.
        /// </summary>
        private Func<object, string, Task<IEnumerable<string?>>> FieldValidationAdapter =>
            _fieldValidationAdapter ??= async (model, memberPath) =>
            {
                var failures = await GetModelFailuresTaskAsync(model);

                return failures
                    .Where(f => MatchesMemberPath(f.PropertyName, memberPath))
                    .Select(f => (string?)f.ErrorMessage);
            };

        private Task<IReadOnlyList<ValidationFailure>> GetModelFailuresTaskAsync(object? model)
        {
            if (model is not TModel typed)
                return Task.FromResult<IReadOnlyList<ValidationFailure>>([]);

            if (ReferenceEquals(typed, _cachedModel) && IsCacheFresh() && _cachedFailures is not null)
                return Task.FromResult(_cachedFailures);

            // Deduplicate parallel calls (base.ValidateAsync validates every field concurrently).
            if (_inFlight is not null && ReferenceEquals(_inFlightModel, typed))
                return _inFlight;

            _inFlightModel = typed;
            _inFlight = ValidateAndCacheAsync(typed);

            return _inFlight;
        }

        private async Task<IReadOnlyList<ValidationFailure>> ValidateAndCacheAsync(TModel model)
        {
            try
            {
                var failures = await ValidateModelAsync(model);
                _cachedModel = model;
                _cachedFailures = failures;
                _cachedAt = Environment.TickCount64;
                return failures;
            }
            finally
            {
                _inFlight = null;
                _inFlightModel = default;
            }
        }

        private async Task<IReadOnlyList<ValidationFailure>> ValidateModelAsync(TModel model)
        {
            var failures = new List<ValidationFailure>();

            if (UseFluentValidation)
            {
                foreach (var validator in ResolveValidators())
                {
                    var result = await validator.ValidateAsync(model);
                    failures.AddRange(result.Errors);
                }
            }

            if (UseDataAnnotations && model != null)
            {
                failures.AddRange(ModelValidationExtensions.GetDataAnnotationsFailures(model));
            }

            return failures
                .GroupBy(f => (f.PropertyName, f.ErrorMessage))
                .Select(g => g.First())
                .ToList();
        }

        private IEnumerable<IValidator<TModel>> ResolveValidators()
        {
            if (Validator is not null)
            {
                yield return Validator;
                yield break;
            }

            var registered = ServiceProvider.GetService<IEnumerable<IValidator<TModel>>>();
            if (registered is null)
                yield break;

            foreach (var validator in registered)
                yield return validator;
        }

        private bool IsCacheFresh() =>
            ValidationCacheMilliseconds <= 0 || Environment.TickCount64 - _cachedAt <= ValidationCacheMilliseconds;

        private void InvalidateModelValidationCache()
        {
            _cachedModel = default;
            _cachedFailures = null;
            _cachedAt = 0;
            _inFlight = null;
            _inFlightModel = default;
        }

        private static bool MatchesAnyControlPath(ValidationFailure failure, string[] controlPaths) =>
            !string.IsNullOrEmpty(failure.PropertyName) &&
            controlPaths.Any(path => MatchesMemberPath(failure.PropertyName, path));

        private static bool MatchesMemberPath(string? failureProperty, string memberPath)
        {
            if (string.IsNullOrEmpty(failureProperty) || memberPath.Length == 0)
                return false;

            if (string.Equals(failureProperty, memberPath, StringComparison.Ordinal))
                return true;

            // FluentValidation may report a relative name for child validators (Address -> "City")
            // or a full name ("Address.City"); a suffix match covers both.
            return memberPath.EndsWith("." + failureProperty, StringComparison.Ordinal)
                || failureProperty.EndsWith("." + memberPath, StringComparison.Ordinal);
        }

        private IEnumerable<string> GetControlMemberPaths()
        {
            foreach (var control in _formControls)
            {
                var path = GetControlMemberPath(control);
                if (!string.IsNullOrEmpty(path))
                    yield return path;
            }
        }

        /// <summary>
        /// Extracts the full member path (e.g. "Name" or "Address.City") from a form control's
        /// <c>For</c> expression, mirroring the logic of <see cref="ExpressionExtensions.GetFullPathOfMember{T}"/>
        /// (IFormComponent does not expose For, hence the reflection).
        /// </summary>
        private static string? GetControlMemberPath(IFormComponent control)
        {
#pragma warning disable IL2075 // 'For' is always assigned from user markup, so it cannot be trimmed.
            if (control.GetType().GetProperty("For")?.GetValue(control) is not LambdaExpression lambda)
                return null;
#pragma warning restore IL2075

            var body = lambda.Body;
            if (body is UnaryExpression { NodeType: ExpressionType.Convert or ExpressionType.ConvertChecked } unary)
                body = unary.Operand;

            var result = string.Empty;
            var member = body as MemberExpression;
            while (member is not null)
            {
                if (member.Expression is MemberExpression)
                    result = member.Member.Name + (result.Length > 0 ? "." : string.Empty) + result;

                member = member.Expression as MemberExpression;
            }

            return result;
        }
    }
}
