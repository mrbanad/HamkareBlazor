// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


namespace HamkareBlazor;

/// <summary>
/// Information about a requested step change when <see cref="HamkareStepper.OnPreviewInteraction"/> occurs.
/// </summary>
public class StepperInteractionEventArgs
{
    /// <summary>
    /// The desired step index.
    /// </summary>
    public int StepIndex { get; init; }

    /// <summary>
    /// The requested step action.
    /// </summary>
    public StepAction Action { get; set; }

    /// <summary>
    /// Whether to disallow this request.
    /// </summary>
    /// <remarks>
    /// Set this to <c>true</c> to indicate that the requested step change should not occur.
    /// </remarks>
    public bool Cancel { get; set; }
}
