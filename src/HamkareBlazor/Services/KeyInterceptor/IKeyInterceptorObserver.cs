// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;

/// <summary>
/// Represents an observer that observes and responds to key up and down events.
/// </summary>
public interface IKeyInterceptorObserver : IKeyDownObserver, IKeyUpObserver
{
    /// <summary>
    /// Gets the ID of the ancestor HTML element associated with this observer.
    /// This ID should be a unique identifier.
    /// </summary>
    string ElementId { get; }
}
