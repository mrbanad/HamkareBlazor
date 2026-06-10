// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace HamkareBlazor;


/// <summary>
/// The information related to a <see cref="HamkareDropZone{T}"/> index change event.
/// </summary>
public class HamkareDragAndDropIndexChangedEventArgs : EventArgs
{
    /// <summary>
    /// The unique identifier of the zone.
    /// </summary>
    public string ZoneIdentifier { get; }

    /// <summary>
    /// The index of the zone.
    /// </summary>
    public int Index { get; }

    /// <summary>
    /// The unique identifier of the previous zone.
    /// </summary>
    public string OldZoneIdentifier { get; }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="zoneIdentifier">The unique identifier of the zone.</param>
    /// <param name="oldZoneIdentifier">The unique identifier of the previous zone.</param>
    /// <param name="index">The index of the zone.</param>
    public HamkareDragAndDropIndexChangedEventArgs(string zoneIdentifier, string oldZoneIdentifier, int index)
    {
        ZoneIdentifier = zoneIdentifier;
        Index = index;
        OldZoneIdentifier = oldZoneIdentifier;
    }
}
