using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
    /// <summary>
    /// Represents the top portion of a <see cref="HamkareCard"/>.
    /// </summary>
    /// <seealso cref="HamkareCard" />
    /// <seealso cref="HamkareCardActions" />
    /// <seealso cref="HamkareCardContent" />
    /// <seealso cref="HamkareCardMedia" />
    public partial class HamkareCardHeader : HamkareComponentBase
    {
        protected string Classname => new CssBuilder("hamkare-card-header")
            .AddClass("hamkare-card-header-padding", ParentCard?.ContentPadding ?? true)
            .AddClass(Class)
            .Build();

        [CascadingParameter]
        private HamkareCard? ParentCard { get; set; }

        /// <summary>
        /// The avatar to display within this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? CardHeaderAvatar { get; set; }

        /// <summary>
        /// The main content of this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? CardHeaderContent { get; set; }

        /// <summary>
        /// The actions displayed within this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? CardHeaderActions { get; set; }

        /// <summary>
        /// The custom content within this header.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public RenderFragment? ChildContent { get; set; }
    }
}
