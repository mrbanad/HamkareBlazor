// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace HamkareBlazor;

#nullable enable

/// <summary>
/// Represents a divider between breadcrumb items.
/// </summary>
/// <seealso cref="HamkareBreadcrumbs" />
/// <seealso cref="BreadcrumbItem" />
/// <seealso cref="BreadcrumbLink" />
public partial class BreadcrumbSeparator
{
    /// <summary>
    /// The parent breadcrumb component.
    /// </summary>
    [CascadingParameter]
    public HamkareBreadcrumbs? Parent { get; set; }
}
