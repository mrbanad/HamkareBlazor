// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;

#nullable enable
/// <summary>
/// Represents a container for <see cref="IHamkarePopoverHolder"/>, along with the associated <see cref="PopoverHolderOperation"/>.
/// </summary>
public class PopoverHolderContainer
{
    /// <summary>
    /// Gets the operation associated with the container.
    /// </summary>
    public PopoverHolderOperation Operation { get; }

    /// <summary>
    /// Gets the collection of popover holders in the container.
    /// </summary>
    /// <remarks>
    /// Currently, the collection always contains one item.
    /// However, in the future, the behavior might change, and a list of updated states could be sent if the decision is made to update by batches.
    /// </remarks>
    public IReadOnlyCollection<IHamkarePopoverHolder> Holders { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="PopoverHolderContainer"/> class.
    /// </summary>
    /// <param name="operation">The operation associated with the container.</param>
    /// <param name="holders">The collection of <see cref="IHamkarePopoverHolder"/>.</param>
    public PopoverHolderContainer(PopoverHolderOperation operation, IReadOnlyCollection<IHamkarePopoverHolder> holders)
    {
        Holders = holders;
        Operation = operation;
    }
}
