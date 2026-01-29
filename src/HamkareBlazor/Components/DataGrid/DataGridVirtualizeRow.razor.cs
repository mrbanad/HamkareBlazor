// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

#nullable enable
namespace HamkareBlazor
{
    public partial class DataGridVirtualizeRow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : HamkareComponentBase
    {
        [Parameter, EditorRequired]
        [Category(CategoryTypes.DataGrid.Data)]
        public HamkareDataGrid<T> DataGrid { get; set; } = null!;

        [Parameter]
        [Category(CategoryTypes.DataGrid.Grouping)]
        public IGrouping<object, T>? GroupedItems { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Selecting)]
        public EventCallback<(MouseEventArgs args, T item, int index)> RowClick { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Selecting)]
        public EventCallback<(MouseEventArgs args, T item, int index)> ContextRowClick { get; set; }
    }
}
