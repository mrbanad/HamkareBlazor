using Microsoft.AspNetCore.Components;
using HamkareBlazor.Extensions;
using HamkareBlazor.Utilities;

namespace HamkareBlazor
{

    /// <summary>
    /// A container for a <see cref="HamkareDrawer"/> component.
    /// </summary>
    /// <seealso cref="HamkareDrawer"/>
    /// <seealso cref="HamkareDrawerHeader"/>
    public partial class HamkareDrawerContainer : HamkareComponentBase
    {
        protected bool Fixed { get; set; } = false;
        private readonly List<HamkareDrawer> _drawers = new();

        protected virtual string Classname =>
            new CssBuilder()
                .AddClass(GetDrawerClass(FindLeftDrawer()))
                .AddClass(GetDrawerClass(FindRightDrawer()))
                .AddClass(Class)
                .Build();

        protected string Stylename =>
            new StyleBuilder()
                .AddStyle("--hamkare-drawer-width-left", GetDrawerWidth(FindLeftDrawer()), !string.IsNullOrEmpty(GetDrawerWidth(FindLeftDrawer())))
                .AddStyle("--hamkare-drawer-width-right", GetDrawerWidth(FindRightDrawer()), !string.IsNullOrEmpty(GetDrawerWidth(FindRightDrawer())))
                .AddStyle("--hamkare-drawer-height-top", GetDrawerHeight(FindTopDrawer()), !string.IsNullOrEmpty(GetDrawerHeight(FindTopDrawer())))
                .AddStyle("--hamkare-drawer-height-bottom", GetDrawerHeight(FindBottomDrawer()), !string.IsNullOrEmpty(GetDrawerHeight(FindBottomDrawer())))
                .AddStyle("--hamkare-drawer-width-mini-left", GetMiniDrawerWidth(FindLeftMiniDrawer()), !string.IsNullOrEmpty(GetMiniDrawerWidth(FindLeftMiniDrawer())))
                .AddStyle("--hamkare-drawer-width-mini-right", GetMiniDrawerWidth(FindRightMiniDrawer()), !string.IsNullOrEmpty(GetMiniDrawerWidth(FindRightMiniDrawer())))
                .AddStyle(Style)
                .Build();

        /// <summary>
        /// Displays drawers right-to-left.
        /// </summary>
        [CascadingParameter(Name = "RightToLeft")]
        public bool RightToLeft { get; set; }

        /// <summary>
        /// The custom content inside this drawer.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.Drawer.Behavior)]
        public RenderFragment? ChildContent { get; set; }

        internal void Add(HamkareDrawer drawer)
        {
            if (Fixed && !drawer.IsFixed)
                return;

            _drawers.Add(drawer);
            StateHasChanged();
        }

        internal void Remove(HamkareDrawer drawer)
        {
            _drawers.Remove(drawer);
            StateHasChanged();
        }

        private static string GetDrawerClass(HamkareDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            var className = $"hamkare-drawer-{(drawer.GetState<bool>(nameof(HamkareDrawer.Open)) ? "open" : "close")}-{drawer.Variant.ToStringFast(true)}";
            if (drawer.Variant is DrawerVariant.Responsive or DrawerVariant.Mini)
            {
                className += $"-{drawer.Breakpoint.ToStringFast(true)}";
            }
            className += $"-{drawer.GetPosition()}";

            className += $" hamkare-drawer-{drawer.GetPosition()}-clipped-{drawer.ClipMode.ToStringFast(true)}";

            return className;
        }

        private static string? GetDrawerWidth(HamkareDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            return drawer.Width;
        }

        private static string? GetDrawerHeight(HamkareDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            return drawer.Height;
        }

        private static string? GetMiniDrawerWidth(HamkareDrawer? drawer)
        {
            if (drawer is null)
            {
                return string.Empty;
            }

            return drawer.MiniWidth;
        }

        private HamkareDrawer? FindLeftDrawer()
        {
            var anchor = RightToLeft ? Anchor.End : Anchor.Start;

            return _drawers.FirstOrDefault(d => d.Anchor == anchor || d.Anchor == Anchor.Left);
        }

        private HamkareDrawer? FindRightDrawer()
        {
            var anchor = RightToLeft ? Anchor.Start : Anchor.End;

            return _drawers.FirstOrDefault(d => d.Anchor == anchor || d.Anchor == Anchor.Right);
        }

        private HamkareDrawer? FindTopDrawer()
        {
            return _drawers.FirstOrDefault(d => d.Anchor == Anchor.Top);
        }

        private HamkareDrawer? FindBottomDrawer()
        {
            return _drawers.FirstOrDefault(d => d.Anchor == Anchor.Bottom);
        }

        private HamkareDrawer? FindLeftMiniDrawer()
        {
            var anchor = RightToLeft ? Anchor.End : Anchor.Start;

            return _drawers.FirstOrDefault(d => d.Variant == DrawerVariant.Mini && (d.Anchor == anchor || d.Anchor == Anchor.Left));
        }

        private HamkareDrawer? FindRightMiniDrawer()
        {
            var anchor = RightToLeft ? Anchor.Start : Anchor.End;

            return _drawers.FirstOrDefault(d => d.Variant == DrawerVariant.Mini && (d.Anchor == anchor || d.Anchor == Anchor.Right));
        }
    }
}
