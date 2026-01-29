// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;

/// <summary>
/// The content within a <see cref="HamkarePicker{T}"/>.
/// </summary>
/// <seealso cref="HamkarePicker{T}" />
/// <seealso cref="HamkarePickerToolbar" />
#nullable enable
public partial class HamkarePickerContent : HamkareComponentBase
{
    protected string Classname =>
        new CssBuilder("hamkare-picker-content")
            .AddClass(Class)
            .Build();

    /// <summary>
    /// The content to display.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.Picker.Behavior)]
    public RenderFragment? ChildContent { get; set; }
}
