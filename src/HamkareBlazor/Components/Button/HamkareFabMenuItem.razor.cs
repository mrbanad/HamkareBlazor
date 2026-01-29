#nullable enable
using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;

/// <summary>
/// Represents an item for the <see cref="HamkareFabMenu"/>.
/// </summary>
public partial class HamkareFabMenuItem : HamkareFab
{
    private new string Classname => new CssBuilder(base.Classname)
        .AddClass("hamkare-fab-menu-item")
        .AddClass(Class)
        .Build();

    /// <summary>
    /// The size of the menu item.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="Size.Medium"/>.
    /// </remarks>
    [Parameter, Category(CategoryTypes.Button.Appearance)]
    public override Size Size { get; set; } = Size.Medium;
}
