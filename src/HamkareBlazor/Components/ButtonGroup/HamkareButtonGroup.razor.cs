using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
#nullable enable
    /// <summary>
    /// Groups related <see cref="HamkareButton"/> components together visually.
    /// </summary>
    /// <seealso cref="HamkareButton" />
    /// <seealso cref="HamkareToggleGroup{T}"/>
    public partial class HamkareButtonGroup : HamkareComponentBase
    {
        protected string Classname => new CssBuilder("hamkare-button-group-root")
            .AddClass($"hamkare-button-group-override-styles", OverrideStyles)
            .AddClass($"hamkare-button-group-{Variant.ToStringFast(true)}")
            .AddClass($"hamkare-button-group-{Variant.ToStringFast(true)}-{Color.ToStringFast(true)}")
            .AddClass($"hamkare-button-group-{Variant.ToStringFast(true)}-size-{Size.ToStringFast(true)}")
            .AddClass("hamkare-button-group-vertical", Vertical)
            .AddClass("hamkare-button-group-horizontal", !Vertical)
            .AddClass("hamkare-button-group-disable-elevation", !DropShadow)
            .AddClass("hamkare-button-group-rtl", RightToLeft)
            .AddClass("hamkare-width-full", FullWidth)
            .AddClass(Class)
            .Build();

        [CascadingParameter(Name = "RightToLeft")]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// Overrides individual button styles with this group's style.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c>.  When <c>true</c>, the button styles are defined by this group.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Appearance)]
        public bool OverrideStyles { get; set; } = true;

        /// <summary>
        /// The custom content within this group.
        /// </summary>
        /// <remarks>
        /// This property allows for custom content to displayed inside of the group, but is not required.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Behavior)]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// Displays buttons vertically.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>false</c>.  When <c>true</c>, buttons will be displayed vertically, otherwise horizontally.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Appearance)]
        public bool Vertical { get; set; }

        /// <summary>
        /// Displays a shadow.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>true</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Appearance)]
        public bool DropShadow { get; set; } = true;

        /// <summary>
        /// The color of all buttons in this group.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Default" />.  Theme colors are supported.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Appearance)]
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// The size of all buttons in the group.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Size.Medium"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Appearance)]
        public Size Size { get; set; } = Size.Medium;

        /// <summary>
        /// The display variant of all buttons in the group.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Variant.Text"/>.  Other supported values are <see cref="Variant.Outlined"/> and <see cref="Variant.Filled"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Appearance)]
        public Variant Variant { get; set; } = Variant.Text;

        /// <summary>
        /// If true, the button group will take up 100% of available width.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.ButtonGroup.Appearance)]
        public bool FullWidth { get; set; }

        private readonly List<HamkareButton> _renderedButtons = [];

        internal void AddButton(HamkareButton button)
        {
            _renderedButtons.Add(button);
        }

        internal void RemoveButton(HamkareButton button)
        {
            _renderedButtons.Remove(button);
        }

        internal bool NoneButtonIsStreched()
        {
            return !_renderedButtons.Any(b => b.FullWidth);
        }
    }
}
