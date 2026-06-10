using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
    /// <summary>
    /// Represents an image displayed as part of a <see cref="HamkareCard"/>.
    /// </summary>
    /// <seealso cref="HamkareCard" />
    /// <seealso cref="HamkareCardActions" />
    /// <seealso cref="HamkareCardContent" />
    /// <seealso cref="HamkareCardHeader" />
    public partial class HamkareCardMedia : HamkareComponentBase
    {
        protected string StyleString => StyleBuilder.Default($"background-image:url(\"{Image}\");height: {Height}px;")
            .AddStyle(Style)
            .Build();

        protected string Classname => new CssBuilder("hamkare-card-media")
            .AddClass(Class)
            .Build();

        /// <summary>
        /// Text for the <c>title</c> attribute which provides a basic tooltip.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public string? Title { get; set; }

        /// <summary>
        /// The URL of the image to display.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public string? Image { get; set; }

        /// <summary>
        /// The height, in pixels, of the <see cref="Image"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>300</c>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Card.Behavior)]
        public int Height { get; set; } = 300;
    }
}
