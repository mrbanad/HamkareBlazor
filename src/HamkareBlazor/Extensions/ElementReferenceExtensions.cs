using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using HamkareBlazor.Interop;

namespace HamkareBlazor
{
#nullable enable
    [ExcludeFromCodeCoverage]
    public static class ElementReferenceExtensions
    {
        [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "<JSRuntime>k__BackingField")]
        private static extern ref IJSRuntime GetJsRuntime(WebElementReferenceContext context);

        internal static IJSRuntime? GetJSRuntime(this ElementReference elementReference)
        {
            if (elementReference.Context is WebElementReferenceContext context)
            {
                var jsRuntime = GetJsRuntime(context);

                return jsRuntime;
            }

            return null;
        }

        public static ValueTask HamkareFocusFirstAsync(this ElementReference elementReference, int skip = 0, int min = 0) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.focusFirst", elementReference, skip, min) ?? ValueTask.CompletedTask;

        public static ValueTask HamkareFocusLastAsync(this ElementReference elementReference, int skip = 0, int min = 0) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.focusLast", elementReference, skip, min) ?? ValueTask.CompletedTask;

        public static ValueTask HamkareSaveFocusAsync(this ElementReference elementReference) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.saveFocus", elementReference) ?? ValueTask.CompletedTask;

        public static ValueTask HamkareRestoreFocusAsync(this ElementReference elementReference) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.restoreFocus", elementReference) ?? ValueTask.CompletedTask;

        public static ValueTask HamkareBlurAsync(this ElementReference elementReference) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.blur", elementReference) ?? ValueTask.CompletedTask;

        public static ValueTask HamkareSelectAsync(this ElementReference elementReference) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.select", elementReference) ?? ValueTask.CompletedTask;

        public static ValueTask HamkareSelectRangeAsync(this ElementReference elementReference, int pos1, int pos2) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.selectRange", elementReference, pos1, pos2) ?? ValueTask.CompletedTask;

        public static ValueTask HamkareChangeCssAsync(this ElementReference elementReference, string css) =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.changeCss", elementReference, css) ?? ValueTask.CompletedTask;

        public static ValueTask<BoundingClientRect> HamkareGetBoundingClientRectAsync(this ElementReference elementReference) =>
            elementReference.GetJSRuntime()?.InvokeAsync<BoundingClientRect>("hamkareElementRef.getBoundingClientRect", elementReference) ?? ValueTask.FromResult(new BoundingClientRect());

        public static ValueTask<int[]> AddDefaultPreventingHandlers(this ElementReference elementReference, string[] eventNames) =>
            elementReference.GetJSRuntime()?.InvokeAsync<int[]>("hamkareElementRef.addDefaultPreventingHandlers", elementReference, eventNames) ?? new ValueTask<int[]>(Array.Empty<int>());

        public static ValueTask RemoveDefaultPreventingHandlers(this ElementReference elementReference, string[] eventNames, int[] listenerIds)
        {
            if (eventNames.Length != listenerIds.Length)
            {
                throw new ArgumentException($"Number of elements in {nameof(eventNames)} and {nameof(listenerIds)} has to match.");
            }

            return elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.removeDefaultPreventingHandlers", elementReference, eventNames, listenerIds) ?? ValueTask.CompletedTask;
        }

        public static ValueTask HamkareAttachBlurEventWithJS<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] T>(
            this ElementReference elementReference,
            DotNetObjectReference<T> obj) where T : class =>
            elementReference.GetJSRuntime()?.InvokeVoidAsync("hamkareElementRef.addOnBlurEvent", elementReference, obj) ?? ValueTask.CompletedTask;
    }
}
