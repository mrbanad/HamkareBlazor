// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using NetEscapades.EnumGenerators;

namespace HamkareBlazor;

/// <summary>
/// Indicates the column resizing behavior for a <see cref="HamkareDataGrid{T}"/>.
/// </summary>
[EnumExtensions]
public enum ResizeMode
{
    /// <summary>
    /// Nothing happens when the grid is resized.
    /// </summary>
    [Description("none")]
    None,

    /// <summary>
    /// Columns can be expanded a limited amount, ensuring all columns remain visible.
    /// </summary>
    [Description("column")]
    Column,

    /// <summary>
    /// Columns can be expanded any amount; the grid width will be expanded.
    /// </summary>
    [Description("container")]
    Container
}
