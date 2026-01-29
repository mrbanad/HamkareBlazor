// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;
#nullable enable

/// <summary>
/// Indicates how values are edited for <see cref="HamkareDataGrid{T}"/> cells.
/// </summary>
public enum DataGridEditMode
{
    /// <summary>
    /// Values are edited in the cell.
    /// </summary>
    Cell,

    /// <summary>
    /// A dialog is shown to edit values.
    /// </summary>
    Form
}
