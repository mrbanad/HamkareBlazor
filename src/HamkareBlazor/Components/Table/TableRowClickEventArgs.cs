using System;
using Microsoft.AspNetCore.Components.Web;

namespace HamkareBlazor;


/// <summary>
/// The information passed during a <see cref="HamkareTable{T}"/> row click event.
/// </summary>
/// <typeparam name="T">The type of data displayed in the table.</typeparam>
public class TableRowClickEventArgs<T> : EventArgs
{
    /// <summary>
    /// The coordinates of the click.
    /// </summary>
    public MouseEventArgs MouseEventArgs { get; }

    /// <summary>
    /// The row which was clicked.
    /// </summary>
    public HamkareTr Row { get; }

    /// <summary>
    /// The data related to the row which was clicked.
    /// </summary>
    public T? Item { get; }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="mouseEventArgs">The coordinates of the click.</param>
    /// <param name="row">The row which was clicked.</param>
    /// <param name="item">The data related to the row which was clicked.</param>
    public TableRowClickEventArgs(MouseEventArgs mouseEventArgs, HamkareTr row, T? item)
    {
        MouseEventArgs = mouseEventArgs;
        Row = row;
        Item = item;
    }
}
