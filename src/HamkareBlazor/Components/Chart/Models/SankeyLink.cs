// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor.Charts;

#nullable enable
/// <summary>
/// Represents a link in a Sankey diagram, defining the source and target edges.
/// </summary>
/// <param name="Source">The source <see cref="SankeyNode"/>.</param>
/// <param name="Target">The target <see cref="SankeyNode"/>.</param>
public record struct SankeyLink(string Source, string Target);
