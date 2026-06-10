// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor.Utilities
{
    /// <summary>
    /// Represents the information related to a <see cref="HamkareDataGrid{T}.HierarchyVisibilityToggled"/> event.
    /// </summary>
    /// <typeparam name="T">The item managed by the <see cref="HamkareDataGrid{T}"/>.</typeparam>
    public class DataGridHierarchyVisibilityToggledEventArgs<T>
    {
        /// <summary>
        /// The item whose visibility was changed.
        /// </summary>
        public T Item { get; }
        /// <summary>
        /// If <c>true</c> item was expanded, otherwise collapsed
        /// </summary>
        public bool Expanded { get; }

        public DataGridHierarchyVisibilityToggledEventArgs(T item, bool expanded)
        {
            Item = item;
            Expanded = expanded;
        }
    }
}
