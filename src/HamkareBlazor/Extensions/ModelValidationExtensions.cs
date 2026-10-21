// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;

namespace HamkareBlazor
{
    /// <summary>
    /// Runs DataAnnotations validation (<c>Validator.TryValidateObject</c>) over any model and exposes the
    /// messages, optionally mapped to <see cref="ValidationFailure"/> so they can be merged with
    /// FluentValidation results.
    /// </summary>
    /// <remarks>
    /// Property level attributes are usually already validated per field by the form inputs themselves;
    /// this helper additionally covers <c>IValidatableObject</c> implementations and class level
    /// <see cref="ValidationAttribute"/>s, which no single field can display.
    /// </remarks>
    public static class ModelValidationExtensions
    {
        /// <summary>
        /// Validates <paramref name="model"/> with DataAnnotations and returns the failures
        /// (member name + message), ready to be merged with FluentValidation failures.
        /// </summary>
        public static IReadOnlyList<ValidationFailure> GetDataAnnotationsFailures(object model)
        {
            ArgumentNullException.ThrowIfNull(model);

            var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
            var context = new ValidationContext(model, serviceProvider: null, items: null);

            try
            {
                Validator.TryValidateObject(model, context, results, validateAllProperties: true);
            }
            catch (Exception e)
            {
                // A broken attribute or IValidatableObject must not break the whole form validation pipeline.
                return [new ValidationFailure(string.Empty, "Error in DataAnnotations validation: " + e.Message)];
            }

            return results
                .Where(r => !string.IsNullOrWhiteSpace(r.ErrorMessage))
                .SelectMany(r => r.MemberNames.Any()
                    ? r.MemberNames.Select(member => new ValidationFailure(member, r.ErrorMessage!.Trim()))
                    : [new ValidationFailure(string.Empty, r.ErrorMessage!.Trim())])
                .GroupBy(f => (f.PropertyName, f.ErrorMessage))
                .Select(g => g.First())
                .ToList();
        }

        /// <summary>
        /// Validates <paramref name="model"/> with DataAnnotations and returns only the messages.
        /// </summary>
        public static IReadOnlyList<string> GetDataAnnotationsMessages(object model) =>
            GetDataAnnotationsFailures(model)
                .Select(f => f.ErrorMessage)
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .Distinct()
                .ToList();
    }
}
