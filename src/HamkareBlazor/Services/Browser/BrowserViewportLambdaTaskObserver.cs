// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using HamkareBlazor.Services;

namespace HamkareBlazor;

#nullable enable
internal class BrowserViewportLambdaTaskObserver : IBrowserViewportObserver
{
    private readonly Func<BrowserViewportEventArgs, Task> _lambda;

    /// <inheritdoc />
    public Guid Id { get; }

    /// <inheritdoc />
    public ResizeOptions? ResizeOptions { get; }

    public BrowserViewportLambdaTaskObserver(Guid id, Func<BrowserViewportEventArgs, Task> lambda, ResizeOptions? options)
    {
        Id = id;
        ResizeOptions = options;
        _lambda = lambda;
    }

    /// <inheritdoc />
    public Task NotifyBrowserViewportChangeAsync(BrowserViewportEventArgs browserViewportEventArgs)
    {
        return _lambda(browserViewportEventArgs);
    }
}
