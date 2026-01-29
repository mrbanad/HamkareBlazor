using Microsoft.AspNetCore.Components;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;

#nullable enable

/// <summary>
/// A header cell which labels a column of data for a <see cref="HamkareTable{T}"/>.
/// </summary>
public partial class HamkareTh : HamkareComponentBase
{
    protected string Classname => new CssBuilder("hamkare-table-cell")
        .AddClass(Context?.Table?.CellClass)
        .AddClass(Class)
        .Build();

    /// <summary>
    /// The current state of the <see cref="HamkareTable{T}"/> containing this group.
    /// </summary>
    [CascadingParameter]
    public TableContext? Context { get; set; }

    /// <summary>
    /// The content within this header cell.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
