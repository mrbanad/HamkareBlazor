// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace HamkareBlazor.Components.Snackbar.InternalComponents;

public partial class SnackbarMessageText : ComponentBase
{
    /// <summary>
    /// Plain text message to be displayed.
    /// </summary>
    /// <remarks>
    /// This property is used to pass a plain string message. It does not support HTML or UI fragments.
    /// </remarks>
    [Parameter]
    public string? Message { get; set; }
}
