using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
    /// <summary>
    /// Represents a set of buttons displayed as part of a <see cref="HamkareCard"/>.
    /// </summary>
    /// <seealso cref="HamkareCard" />
    /// <seealso cref="HamkareCardContent" />
    /// <seealso cref="HamkareCardHeader" />
    /// <seealso cref="HamkareCardMedia" />
    public partial class HamkareCardActions : HamkareComponentBase
    {
        protected string Classname => new CssBuilder("hamkare-card-actions")
            .AddClass("hamkare-card-actions-padding", ParentCard?.ContentPadding ?? true)
            .AddClass(Class)
            .Build();

        [CascadingParameter]
        private HamkareCard? ParentCard { get; set; }

        /// <summary>
        /// The content within this component.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? ChildContent { get; set; }
    }
}
