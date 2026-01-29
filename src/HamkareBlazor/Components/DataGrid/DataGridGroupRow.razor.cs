// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HamkareBlazor.Utilities;

#nullable enable

namespace HamkareBlazor
{
    public partial class DataGridGroupRow<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T> : HamkareComponentBase
    {
        internal bool _expanded;

        protected string GroupClassname => new CssBuilder("hamkare-table-cell")
            .AddClass("hamkare-datagrid-group")
            .AddClass($"hamkare-row-group-indented-{(GroupDefinition.Indentation ? Math.Min(GroupDefinition.Level, 5) : 0)}")
            .AddClass(GroupClassFunc?.Invoke(GroupDefinition))
            .AddClass(GroupClass)
            .Build();

        protected string GroupStylename => new StyleBuilder()
            .AddStyle(GroupStyle)
            .AddStyle(GroupStyleFunc?.Invoke(GroupDefinition))
            .Build();

        [Parameter, EditorRequired]
        [Category(CategoryTypes.DataGrid.Grouping)]
        public HamkareDataGrid<T> DataGrid { get; set; } = null!;

        [Parameter]
        [Category(CategoryTypes.DataGrid.Selecting)]
        public EventCallback<(MouseEventArgs args, T item, int index)> RowClick { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Selecting)]
        public EventCallback<(MouseEventArgs args, T item, int index)> ContextRowClick { get; set; }

        /// <summary>
        /// The definition for this grouping level
        /// </summary>
        [Parameter, EditorRequired]
        [Category(CategoryTypes.DataGrid.Grouping)]
        public GroupDefinition<T> GroupDefinition { get; set; } = null!;

        /// <summary>
        /// The groups and items within this grouping.
        /// </summary>
        [Parameter]
        [Category(CategoryTypes.DataGrid.Grouping)]
        public IGrouping<object?, T>? Items { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Appearance)]
        public string? GroupClass { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Appearance)]
        public string? GroupStyle { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Appearance)]
        public Func<GroupDefinition<T>, string>? GroupClassFunc { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Appearance)]
        public Func<GroupDefinition<T>, string>? GroupStyleFunc { get; set; }

        [Parameter]
        [Category(CategoryTypes.DataGrid.Appearance)]
        public string? StyleClass { get; set; }

        protected override void OnParametersSet()
        {
            _expanded = GroupDefinition.Expanded;
            base.OnParametersSet();
        }

        internal void GroupExpandClick()
        {
            _expanded = !_expanded;
            if (Items != null)
                DataGrid.ToggleGroupExpand(GroupDefinition.Title, GroupDefinition.KeyPath, _expanded);
        }
    }
}
