using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Sections;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;

#nullable enable

/// <summary>
/// Displays actions, branding, navigation and screen titles. Keep the app bar persistent while browsing different pages to ease navigation and access to actions for users.
/// </summary>
/// <seealso cref="HamkareContextualActionBar"/>
public partial class HamkareAppBar : HamkareComponentBase
{
    internal static SectionOutlet ContextualActionBar { get; } = new();

    protected string Classname =>
        new CssBuilder("hamkare-appbar")
            .AddClass($"hamkare-appbar-dense", Dense)
            .AddClass($"hamkare-appbar-fixed-top", Fixed && !Bottom)
            .AddClass($"hamkare-appbar-fixed-bottom", Fixed && Bottom)
            .AddClass($"hamkare-elevation-{Elevation}")
            .AddClass($"hamkare-theme-{Color.ToStringFast(true)}", Color != Color.Default)
            .AddClass(Class)
            .Build();

    protected string ToolBarClassname =>
        new CssBuilder("hamkare-toolbar-appbar")
            .AddClass(ToolBarClass)
            .Build();

    /// <summary>
    /// Places the appbar at the bottom of the screen instead of the top.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.AppBar.Behavior)]
    public bool Bottom { get; set; }

    /// <summary>
    /// Allows the app bar to be overridden with page specific actions
    /// </summary>
    /// <remarks>
    /// Defaults to <c>false</c>. When <c>true</c>, can be overridden by <see cref="HamkareContextualActionBar"/>
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Behavior)]
    public bool Contextual { get; set; } = false;

    /// <summary>
    /// The size of the drop shadow.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>4</c>.  A higher number creates a heavier drop shadow.  Use a value of <c>0</c> for no shadow.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Appearance)]
    public int Elevation { set; get; } = 4;

    /// <summary>
    /// Uses compact padding.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>false</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Appearance)]
    public bool Dense { get; set; }

    /// <summary>
    /// Adds left and right padding to this appbar.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>true</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Appearance)]
    public bool Gutters { get; set; } = true;

    /// <summary>
    /// The color of this appbar.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="Color.Default"/>.  Theme colors are supported.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Appearance)]
    public Color Color { get; set; } = Color.Default;

    /// <summary>
    /// Fixes this appbar in place as the page is scrolled.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>true</c>.  When <c>false</c>, the appbar will scroll with other page content.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Behavior)]
    public bool Fixed { get; set; } = true;

    /// <summary>
    /// Allows appbar content to wrap.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>false</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Behavior)]
    public bool WrapContent { get; set; } = false;

    /// <summary>
    /// The CSS classes applied to the nested toolbar.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>null</c>.  You can use spaces to separate multiple classes.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.AppBar.Appearance)]
    public string? ToolBarClass { get; set; }

    /// <summary>
    /// The content within this component.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.AppBar.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
