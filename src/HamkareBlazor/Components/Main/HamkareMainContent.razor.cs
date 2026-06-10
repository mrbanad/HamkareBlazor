// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;

/// <summary>
/// Represents the main content area of the <see cref="HamkareLayout"/>.
/// </summary>
public partial class HamkareMainContent : HamkareComponentBase
{
    /// <summary>
    /// Gets the CSS class names for the component.
    /// </summary>
    protected string Classname =>
        new CssBuilder("hamkare-main-content")
            .AddClass(Class)
            .Build();

    /// <summary>
    /// Sets the content to be rendered inside the main content area.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.MainContent.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
