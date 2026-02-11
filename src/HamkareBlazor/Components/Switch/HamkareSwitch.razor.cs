using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HamkareBlazor.Services;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
#nullable enable

    /// <summary>
    /// Toggles between two values with the tap of a button, visually distinct from checkboxes. Use switches (not radio buttons) if the items in a list can be independently controlled.
    /// </summary>
    /// <typeparam name="T">The kind of value being switched, typically a <see cref="bool"/>.</typeparam>
    /// <seealso cref="HamkareCheckBox{T}"/>
    /// <seealso cref="HamkareRadio{T}"/>
    public partial class HamkareSwitch<T> : HamkareBooleanInput<T>
    {
        private string _elementId = Identifier.Create("switch");

        [Inject]
        private IKeyInterceptorService KeyInterceptorService { get; set; } = null!;

        protected override string Classname => new CssBuilder("hamkare-input-control-boolean-input")
            .AddClass("w-100")
            .AddClass(Class)
            .Build();

        protected override string LabelClassname => new CssBuilder("hamkare-switch")
            .AddClass("hamkare-disabled", GetDisabledState())
            .AddClass("hamkare-readonly", GetReadOnlyState())
            .AddClass($"hamkare-input-content-placement-{ConvertPlacement(LabelPlacement).ToStringFast(true)}")
            .Build();

        protected string SwitchClassname => new CssBuilder("hamkare-button-root hamkare-icon-button hamkare-switch-base")
            .AddClass($"hamkare-ripple hamkare-ripple-switch", Ripple && !GetReadOnlyState() && !GetDisabledState())
            .AddClass($"hamkare-{Color.ToStringFast(true)}-text hover:hamkare-{Color.ToStringFast(true)}-hover", !GetReadOnlyState() && !GetDisabledState() && BoolValue == true)
            .AddClass($"hamkare-{UncheckedColor.ToStringFast(true)}-text hover:hamkare-{UncheckedColor.ToStringFast(true)}-hover", !GetReadOnlyState() && !GetDisabledState() && BoolValue == false)
            .AddClass($"hamkare-switch-disabled", GetDisabledState())
            .AddClass($"hamkare-readonly", GetReadOnlyState())
            .AddClass($"hamkare-checked", BoolValue)
            .AddClass($"hamkare-switch-base-{Size.ToStringFast(true)}")
            .Build();

        protected string TrackClassname => new CssBuilder("hamkare-switch-track")
            .AddClass($"hamkare-{Color.ToStringFast(true)}", BoolValue == true)
            .AddClass($"hamkare-{UncheckedColor.ToStringFast(true)}", BoolValue == false)
            .Build();

        protected string ThumbClassname => new CssBuilder($"hamkare-switch-thumb-{Size.ToStringFast(true)}")
            .AddClass("d-flex align-center justify-center")
            .Build();

        protected string SpanClassname => new CssBuilder("hamkare-switch-span")
            .AddClass($"hamkare-switch-span-{Size.ToStringFast(true)}")
            .Build();

        /// <summary>
        /// The color of this switch when in an unchecked state.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Default"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Radio.Appearance)]
        public Color UncheckedColor { get; set; } = Color.Default;

        /// <summary>
        /// The icon to display for the switch thumb.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Appearance)]
        public string? ThumbIcon { get; set; }

        /// <summary>
        /// The color of the thumb icon.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Default"/>. Only applies when <see cref="ThumbIcon"/> is set.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.FormComponent.Appearance)]
        public Color ThumbIconColor { get; set; } = Color.Default;

        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        /// <param name="obj">Information about which key was pressed.</param>
        /// <remarks>
        /// Supported keys are:<br />
        /// <c>ArrowLeft</c> or <c>Delete</c> to uncheck the switch.<br />
        /// <c>ArrowRight</c>, <c>Enter</c>, or <c>NumpadEnter</c> to check the switch.<br />
        /// <c>Space</c> to toggle the selected value.
        /// </remarks>
        protected internal async Task HandleKeyDownAsync(KeyboardEventArgs obj)
        {
            if (GetDisabledState() || GetReadOnlyState())
            {
                return;
            }

            switch (obj.Key)
            {
                case "ArrowLeft" or "Delete":
                    await SetBoolValueAsync(false, true);
                    break;
                case "ArrowRight" or "Enter" or "NumpadEnter":
                    await SetBoolValueAsync(true, true);
                    break;
                case " ":
                    switch (BoolValue)
                    {
                        case true:
                            await SetBoolValueAsync(false, true);
                            break;
                        default:
                            await SetBoolValueAsync(true, true);
                            break;
                    }

                    break;
            }
        }

        /// <inheritdoc />
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var options = new KeyInterceptorOptions(
                    "hamkare-switch-base",
                    [
                        // prevent scrolling page, instead increment
                        new("ArrowUp", preventDown: "key+none"),
                        // prevent scrolling page, instead decrement
                        new("ArrowDown", preventDown: "key+none"),
                        new(" ", preventDown: "key+none", preventUp: "key+none")
                    ]);

                await KeyInterceptorService.SubscribeAsync(_elementId, options, keyDown: HandleKeyDownAsync);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        /// <inheritdoc />
        protected override async ValueTask DisposeAsyncCore()
        {
            await base.DisposeAsyncCore();

            if (IsJSRuntimeAvailable)
            {
                await KeyInterceptorService.UnsubscribeAsync(_elementId);
            }
        }
    }
}
