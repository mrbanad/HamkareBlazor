// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using HamkareBlazor.Utilities;

namespace HamkareBlazor;

/// <summary>
/// A drag handle that restricts drag-and-drop initiation to a specific child element
/// inside a <see cref="HamkareDynamicDropItem{T}"/>.
/// </summary>
/// <typeparam name="T">The type of item being dragged.</typeparam>
/// <remarks>
/// Place this component anywhere inside a <see cref="HamkareDropContainer{T}.ItemRenderer"/>. Once
/// registered, the parent item's full-element draggable behavior is suppressed so that
/// only interactions with the handle element start a drag-and-drop transaction.
/// <para>
/// Example — make only the card header draggable:
/// <code lang="razor">
/// &lt;HamkareDropZone T="MyItem" ...&gt;
///     &lt;ItemRenderer&gt;
///         &lt;HamkareCard&gt;
///             &lt;HamkareCardHeader&gt;
///                 &lt;HamkareDragHandle T="MyItem"&gt;
///                     &lt;HamkareIcon Icon="@Icons.Material.Filled.DragIndicator" /&gt;
///                 &lt;/HamkareDragHandle&gt;
///                 &lt;HamkareText&gt;@context.Title&lt;/HamkareText&gt;
///             &lt;/HamkareCardHeader&gt;
///             &lt;HamkareCardContent&gt;...&lt;/HamkareCardContent&gt;
///         &lt;/HamkareCard&gt;
///     &lt;/ItemRenderer&gt;
/// &lt;/HamkareDropZone&gt;
/// </code>
/// </para>
/// </remarks>
public partial class HamkareDragHandle<T> : HamkareComponentBase, IDisposable where T : notnull
{
    private bool _disposedValue = false;

    /// <summary>
    /// The parent drop item provided by the <see cref="HamkareDynamicDropItem{T}"/> ancestor.
    /// </summary>
    [CascadingParameter]
    private HamkareDynamicDropItem<T>? DropItem { get; set; }

    /// <summary>
    /// The content displayed inside the drag handle.
    /// </summary>
    [Parameter]
    [Category(CategoryTypes.DropZone.Appearance)]
    public RenderFragment? ChildContent { get; set; }

    protected string Classname =>
        new CssBuilder("hamkare-drag-handle")
            .AddClass(Class)
            .Build();

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        if (DropItem is null)
        {
            throw new InvalidOperationException(
                $"{nameof(HamkareDragHandle<T>)} must be placed inside a {nameof(HamkareDynamicDropItem<T>)}.");
        }

        base.OnInitialized();
        DropItem.RegisterDragHandle();
    }

    private Task OnDragStartedAsync() => DropItem?.DragStartedAsync() ?? Task.CompletedTask;

    private Task OnDragEndedAsync(DragEventArgs e) => DropItem?.DragEndedAsync() ?? Task.CompletedTask;

    private Task OnTouchStartedAsync(TouchEventArgs e) => DropItem?.TouchStartedAsync(e) ?? Task.CompletedTask;

    private Task OnTouchMovedAsync(TouchEventArgs e) => DropItem?.TouchMovedAsync(e) ?? Task.CompletedTask;

    private Task OnTouchEndedAsync(TouchEventArgs e) => DropItem?.TouchEndedAsync(e) ?? Task.CompletedTask;

    /// <summary>
    /// Releases resources used by this drag handle and unregisters it from the parent item.
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                DropItem?.UnregisterDragHandle();
            }

            _disposedValue = true;
        }
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
