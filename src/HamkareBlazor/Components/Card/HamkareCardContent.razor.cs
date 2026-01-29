using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
#nullable enable
    /// <summary>
    /// Represents the primary content displayed within a <see cref="HamkareCard"/>.
    /// </summary>
    /// <seealso cref="HamkareCard" />
    /// <seealso cref="HamkareCardActions" />
    /// <seealso cref="HamkareCardHeader" />
    /// <seealso cref="HamkareCardMedia" />
    public partial class HamkareCardContent : HamkareComponentBase
    {
        protected string Classname => new CssBuilder("hamkare-card-content")
            .AddClass("hamkare-card-content-padding", ParentCard?.ContentPadding ?? true)
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
