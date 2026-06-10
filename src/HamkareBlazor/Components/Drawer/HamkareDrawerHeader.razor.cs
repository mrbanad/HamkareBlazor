// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;


/// <summary>
/// Represents content at the top of a <see cref="HamkareDrawer"/>.
/// </summary>
/// <seealso cref="HamkareDrawer"/>
/// <seealso cref="HamkareDrawerContainer"/>
public partial class HamkareDrawerHeader
{
    protected string Classname =>
      new CssBuilder("hamkare-drawer-header")
          .AddClass($"hamkare-drawer-header-dense", Dense)
          .AddClass(Class)
          .Build();

    /// <summary>
    /// Uses compact padding.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>false</c>.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.Drawer.Appearance)]
    public bool Dense { get; set; }

    /// <summary>
    /// Navigates to the index page on click.
    /// </summary>
    /// <remarks>
    /// Defaults to <c>false</c>.  When <c>true</c>, the component will link to index page upon click.
    /// </remarks>
    [Parameter]
    [Category(CategoryTypes.Drawer.Behavior)]
    public bool LinkToIndex { get; set; }

    /// <summary>
    /// Custom content within this component.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Drawer.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
