// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor.State;

#nullable enable
[Flags]
public enum ParameterUsageOptions
{
    None = 0,
    Read = 1 << 1,
    Write = 1 << 2,
    All = Read | Write
}
