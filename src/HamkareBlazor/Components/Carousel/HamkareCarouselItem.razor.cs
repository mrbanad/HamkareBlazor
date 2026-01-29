using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{
#nullable enable
    /// <summary>
    /// Represents a slide displayed within a <see cref="HamkareCarousel{TData}"/>.
    /// </summary>
    /// <seealso cref="HamkareCarousel{TData}" />
    public partial class HamkareCarouselItem : HamkareComponentBase, IDisposable
    {
        private bool _disposed = false;

        protected string Classname => new CssBuilder("hamkare-carousel-item")
            .AddClass($"hamkare-carousel-item-{Color.ToStringFast(true)}")
            .AddClass("hamkare-carousel-item-exit", !_disposed && Parent?.LastContainer == this)
            .AddClass("hamkare-carousel-transition-fade-in", !_disposed && Transition == Transition.Fade && Parent?.SelectedContainer == this)
            .AddClass("hamkare-carousel-transition-fade-out", !_disposed && Transition == Transition.Fade && Parent?.LastContainer == this)
            .AddClass("hamkare-carousel-transition-slide-next-enter", !_disposed && Transition == Transition.Slide && !RightToLeft && Parent?.SelectedContainer == this && Parent._moveNext)
            .AddClass("hamkare-carousel-transition-slide-next-exit", !_disposed && Transition == Transition.Slide && !RightToLeft && Parent?.LastContainer == this && Parent._moveNext)
            .AddClass("hamkare-carousel-transition-slide-prev-enter", !_disposed && Transition == Transition.Slide && !RightToLeft && Parent?.SelectedContainer == this && !Parent._moveNext)
            .AddClass("hamkare-carousel-transition-slide-prev-exit", !_disposed && Transition == Transition.Slide && !RightToLeft && Parent?.LastContainer == this && !Parent._moveNext)
            .AddClass("hamkare-carousel-transition-slide-next-rtl-enter", !_disposed && Transition == Transition.Slide && RightToLeft && Parent?.SelectedContainer == this && Parent._moveNext)
            .AddClass("hamkare-carousel-transition-slide-next-rtl-exit", !_disposed && Transition == Transition.Slide && RightToLeft && Parent?.LastContainer == this && Parent._moveNext)
            .AddClass("hamkare-carousel-transition-slide-prev-rtl-enter", !_disposed && Transition == Transition.Slide && RightToLeft && Parent?.SelectedContainer == this && !Parent._moveNext)
            .AddClass("hamkare-carousel-transition-slide-prev-rtl-exit", !_disposed && Transition == Transition.Slide && RightToLeft && Parent?.LastContainer == this && !Parent._moveNext)
            .AddClass("hamkare-carousel-transition-none", !_disposed && Transition == Transition.None && Parent?.SelectedContainer != this)
            .AddClass(CustomTransitionEnter, !_disposed && Transition == Transition.Custom && Parent?.SelectedContainer == this && Parent.SelectedContainer == this)
            .AddClass(CustomTransitionExit, !_disposed && Transition == Transition.Custom && Parent?.LastContainer == this && Parent.LastContainer == this)
            .AddClass(Class)
            .Build();

        /// <summary>
        /// The content displayed within this component.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Carousel.Behavior)]
        public RenderFragment? ChildContent { get; set; }

        [CascadingParameter]
        protected internal HamkareBaseItemsControl<HamkareCarouselItem>? Parent { get; set; }

        [CascadingParameter(Name = "RightToLeft")]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// The color of this item. 
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Color.Default"/>.  Theme colors are supported.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Carousel.Appearance)]
        public Color Color { get; set; } = Color.Default;

        /// <summary>
        /// The effect used to blend from this item to a different <see cref="HamkareCarouselItem"/>.
        /// </summary>
        /// <remarks>
        /// Defaults to <see cref="Transition.Slide"/>.
        /// </remarks>
        [Parameter]
        [Category(CategoryTypes.Carousel.Appearance)]
        public Transition Transition { get; set; } = Transition.Slide;

        /// <summary>
        /// The custom CSS transition used to blend into this carousel item.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Carousel.Appearance)]
        public string? CustomTransitionEnter { get; set; }

        /// <summary>
        /// The custom CSS transition used to blend away from this carousel item.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Carousel.Appearance)]
        public string? CustomTransitionExit { get; set; }

        /// <summary>
        /// Displays this item.
        /// </summary>
        public bool Visible => Parent is not null && (Parent.LastContainer == this || Parent.SelectedIndex == Parent.Items.IndexOf(this));

        /// <inheritdoc />
        protected override Task OnInitializedAsync()
        {
            Parent?.AddItem(this);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Releases resources used by this component.
        /// </summary>
        public void Dispose()
        {
            _disposed = true;
            Parent?.Items.Remove(this);
        }
    }
}
