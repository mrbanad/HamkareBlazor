using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
#nullable enable

    /// <summary>
    /// Displays a placeholder preview of content before the data gets loaded, reducing load-time frustration.
    /// </summary>
    public partial class HamkareSkeleton : HamkareComponentBase
    {
        protected string Classname =>
            new CssBuilder("hamkare-skeleton")
                .AddClass($"hamkare-skeleton-{SkeletonType.ToStringFast(true)}")
                .AddClass($"hamkare-skeleton-{Animation.ToStringFast(true)}")
                .AddClass(Class)
                .Build();

        protected string Stylename =>
            new StyleBuilder()
                .AddStyle("maxWidth", $"{MaxWidth}", !string.IsNullOrEmpty(MaxWidth))
                .AddStyle("MaxHeight", $"{MaxHeight}", !string.IsNullOrEmpty(MaxHeight))
                .AddStyle("border-radius", $"{BorderRadius}", !string.IsNullOrEmpty(BorderRadius))
                .AddStyle("height", $"{Height}", !string.IsNullOrEmpty(Height))
                .AddStyle("width", $"{Width}", !string.IsNullOrEmpty(Width))
                .AddStyle(Style)
                .Build();

        /// <summary>
        /// The width of this skeleton.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  Values can be in pixels (e.g. <c>"300px"</c>) or percentages (e.g. <c>"30%"</c>).
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Skeleton.Appearance)]
        public string? Width { set; get; }

        /// <summary>
        /// The height of this skeleton.
        /// </summary>
        /// <remarks>
        /// Defaults to <c>null</c>.  Values can be in pixels (e.g. <c>"300px"</c>) or percentages (e.g. <c>"30%"</c>).
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Skeleton.Appearance)]
        public string? Height { set; get; }

        /// <summary>
        /// The shape of this skeleton.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="SkeletonType.Rectangle"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Skeleton.Appearance)]
        public SkeletonType SkeletonType { set; get; } = SkeletonType.Rectangle;

        /// <summary>
        /// The type of animation to display.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Animation.Wave"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Skeleton.Appearance)]
        public Animation Animation { set; get; } = Animation.Wave;
        
        [Category(CategoryTypes.Skeleton.Appearance)]
        [Parameter] public string? MaxWidth { get; set; }
        
        [Category(CategoryTypes.Skeleton.Appearance)]
        [Parameter] public string? MaxHeight { get; set; }
        
        [Category(CategoryTypes.Skeleton.Appearance)]
        [Parameter] public string? BorderRadius { get; set; }
    }
}
