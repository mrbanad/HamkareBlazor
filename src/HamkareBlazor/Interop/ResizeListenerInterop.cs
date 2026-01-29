// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.JSInterop;
using HamkareBlazor.Services;

namespace HamkareBlazor.Interop;

#nullable enable
internal class ResizeListenerInterop
{
    private readonly IJSRuntime _jsRuntime;

    public ResizeListenerInterop(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async ValueTask<bool> MatchMedia(string mediaQuery, CancellationToken cancellationToken = default)
    {
        var (success, value) = await _jsRuntime.InvokeAsyncWithErrorHandling(false, "hamkareResizeListener.matchMedia", cancellationToken, mediaQuery);

        return value;
    }

    public ValueTask<bool> ListenForResize<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] T>(DotNetObjectReference<T> dotNetObjectReference, ResizeOptions options, Guid javaScriptListerId, CancellationToken cancellationToken = default) where T : class
    {
        return _jsRuntime.InvokeVoidAsyncWithErrorHandling("hamkareResizeListenerFactory.listenForResize", cancellationToken, dotNetObjectReference, options, javaScriptListerId);
    }

    public ValueTask<bool> CancelListener(Guid javaScriptListerId, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsyncWithErrorHandling("hamkareResizeListenerFactory.cancelListener", cancellationToken, javaScriptListerId);
    }

    [ExcludeFromCodeCoverage(Justification = "Not used in the core for now.")]
    public ValueTask<bool> CancelListeners(Guid[] jsListenerIds, CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsyncWithErrorHandling("hamkareResizeListenerFactory.cancelListeners", cancellationToken, jsListenerIds);
    }

    public ValueTask Dispose(CancellationToken cancellationToken = default)
    {
        return _jsRuntime.InvokeVoidAsyncIgnoreErrors("hamkareResizeListenerFactory.dispose", cancellationToken);
    }

    public async ValueTask<BrowserWindowSize> GetBrowserWindowSize(CancellationToken cancellationToken = default)
    {
        var (success, value) = await _jsRuntime.InvokeAsyncWithErrorHandling(new BrowserWindowSize(), "hamkareResizeListener.getBrowserWindowSize", cancellationToken);

        return value;
    }
}
