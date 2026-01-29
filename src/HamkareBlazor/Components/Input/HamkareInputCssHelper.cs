using HamkareBlazor.Utilities;

namespace HamkareBlazor;

#nullable enable
/// <summary>
/// A set of methods which generate CSS classes for <see cref="HamkareBaseInput{T}" /> components.
/// </summary>
internal static class HamkareInputCssHelper
{
    /// <summary>
    /// Gets the CSS classes for the specified input component.
    /// </summary>
    /// <typeparam name="T">The type of data collect by the input.</typeparam>
    /// <param name="baseInput">The input control to use.</param>
    /// <param name="shrinkWhen">The function which determines when to shrink the input.</param>
    /// <returns>A set of CSS classes.</returns>
    public static string GetClassname<T>(HamkareBaseInput<T> baseInput, Func<bool> shrinkWhen) =>
        new CssBuilder("hamkare-input")
            .AddClass($"hamkare-input-{baseInput.Variant.ToStringFast(true)}")
            .AddClass($"hamkare-input-{baseInput.Variant.ToStringFast(true)}-with-label", !string.IsNullOrEmpty(baseInput.Label))
            .AddClass($"hamkare-input-adorned-{baseInput.Adornment.ToStringFast(true)}", baseInput.Adornment != Adornment.None)
            .AddClass($"hamkare-input-margin-{baseInput.Margin.ToStringFast(true)}", () => baseInput.Margin != Margin.None)
            .AddClass("hamkare-input-underline", () => baseInput.Underline && baseInput.Variant != Variant.Outlined)
            .AddClass("hamkare-shrink", shrinkWhen)
            .AddClass("hamkare-disabled", baseInput.Disabled)
            .AddClass("hamkare-input-error", baseInput.HasErrors)
            .AddClass("hamkare-ltr", baseInput.GetInputType() == InputType.Email || baseInput.GetInputType() == InputType.Telephone)
            .AddClass($"hamkare-typography-{baseInput.Typo.ToStringFast(true)}")
            .AddClass(baseInput.Class)
            .Build();

    /// <summary>
    /// Gets the CSS classes for the specified input component slot.
    /// </summary>
    /// <typeparam name="T">The type of data collect by the input.</typeparam>
    /// <param name="baseInput">The input control to use.</param>
    /// <returns>A set of CSS classes.</returns>
    public static string GetInputClassname<T>(HamkareBaseInput<T> baseInput) =>
        new CssBuilder("hamkare-input-slot")
            .AddClass("hamkare-input-root")
            .AddClass($"hamkare-input-root-{baseInput.Variant.ToStringFast(true)}")
            .AddClass($"hamkare-input-root-adorned-{baseInput.Adornment.ToStringFast(true)}", baseInput.Adornment != Adornment.None)
            .AddClass($"hamkare-input-root-margin-{baseInput.Margin.ToStringFast(true)}", () => baseInput.Margin != Margin.None)
            .AddClass(baseInput.Class)
            .Build();

    /// <summary>
    /// Gets the CSS classes for the specified input adornment.
    /// </summary>
    /// <typeparam name="T">The type of data collect by the input.</typeparam>
    /// <param name="baseInput">The input control to use.</param>
    /// <returns>A set of CSS classes.</returns>
    public static string GetAdornmentClassname<T>(HamkareBaseInput<T> baseInput) =>
        new CssBuilder()
            .AddClass($"hamkare-input-adornment-{baseInput.Adornment.ToStringFast(true)}", baseInput.Adornment != Adornment.None)
            .AddClass($"hamkare-text", !string.IsNullOrEmpty(baseInput.AdornmentText))
            .AddClass($"hamkare-input-root-filled-shrink", baseInput.Variant == Variant.Filled)
            .AddClass(baseInput.Class)
            .Build();
}
