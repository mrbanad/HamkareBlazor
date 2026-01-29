// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;

#nullable enable

/// <summary>
/// A portion of a <see cref="HamkareGrid"/>.
/// </summary>
/// <seealso cref="HamkareGrid"/>
public partial class HamkareItem : HamkareComponentBase
{
    protected string Classname =>
        new CssBuilder("hamkare-grid-item")
            .AddClass($"hamkare-grid-item-xs-{xs}", xs != 0)
            .AddClass($"hamkare-grid-item-sm-{sm}", sm != 0)
            .AddClass($"hamkare-grid-item-md-{md}", md != 0)
            .AddClass($"hamkare-grid-item-lg-{lg}", lg != 0)
            .AddClass($"hamkare-grid-item-xl-{xl}", xl != 0)
            .AddClass($"hamkare-grid-item-xxl-{xxl}", xxl != 0)
            .AddClass(Class)
            .Build();

    [CascadingParameter]
    private HamkareGrid? Parent { get; set; }

    /// <summary>
    /// Sets the number of columns to occupy at the 'extra small' breakpoint.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public int xs { get; set; }

    /// <summary>
    ///Sets the number of columns to occupy at the 'small' breakpoint.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public int sm { get; set; }

    /// <summary>
    /// Sets the number of columns to occupy at the 'medium' breakpoint.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public int md { get; set; }

    /// <summary>
    /// Sets the number of columns to occupy at the 'large' breakpoint.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public int lg { get; set; }

    /// <summary>
    /// Sets the number of columns to occupy at the 'extra large' breakpoint.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public int xl { get; set; }

    /// <summary>
    /// Sets the number of columns to occupy at the 'extra extra large' breakpoint.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public int xxl { get; set; }

    // ToDo false,auto,true on all sizes.

    /// <summary>
    /// Child content of the component.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Item.Behavior)]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        // NOTE: we can't throw here, the component must be able to live alone for the docs API to infer default parameters
        //if (Parent == null)
        //    throw new ArgumentNullException(nameof(Parent), "Item must exist within a Grid");
        base.OnInitialized();
    }
}
