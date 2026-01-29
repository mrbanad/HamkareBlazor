// Copyright (c) HamkareBlazor 2023
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;

/// <summary>
/// Indicates the requested behavior of a button in a <see cref="HamkareStepper"/> component.
/// </summary>
/// <remarks>
/// Typically called during <see cref="HamkareStepper.OnPreviewInteraction"/> to ask whether step change should be allowed.
/// </remarks>
public enum StepAction
{
    /// <summary>
    /// A request to activate a step.
    /// </summary>
    Activate,

    /// <summary>
    /// A request to complete the last step.
    /// </summary>
    Complete,

    /// <summary>
    /// A request to skip the current step.
    /// </summary>
    Skip,

    /// <summary>
    /// A request to start over at the first step.
    /// </summary>
    Reset
}
