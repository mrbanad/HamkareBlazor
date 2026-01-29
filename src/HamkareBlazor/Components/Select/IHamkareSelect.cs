// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Components;

namespace HamkareBlazor
{
    internal interface IHamkareSelect
    {
        void CheckGenericTypeMatch(object select_item);
        bool MultiSelection { get; set; }
    }

    internal interface IHamkareShadowSelect
    {
    }
}
