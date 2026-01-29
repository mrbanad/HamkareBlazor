// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;
using HamkareBlazor.Interop;

namespace HamkareBlazor.Services;

#nullable enable
/// <summary>
/// Delegate for handling size change events.
/// </summary>
/// <param name="changes">A dictionary containing the elements and their corresponding bounding client rectangles that have changed size.</param>
public delegate void SizeChanged(IDictionary<ElementReference, BoundingClientRect> changes);
