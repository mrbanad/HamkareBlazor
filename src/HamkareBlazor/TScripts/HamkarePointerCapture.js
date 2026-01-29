// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

// Simple helper for pointer capture in DataGrid column resizing
window.hamkarePointerCapture = {
    capture: function (element, pointerId) {
        if (element && typeof element.setPointerCapture === 'function') {
            element.setPointerCapture(pointerId);
        }
    },

    release: function (element, pointerId) {
        if (element && typeof element.releasePointerCapture === 'function') {
            element.releasePointerCapture(pointerId);
        }
    }
};
