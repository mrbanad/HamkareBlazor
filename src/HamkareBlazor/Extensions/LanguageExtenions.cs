// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace HamkareBlazor;

public static class LanguageExtensions
{
    /// <summary>
    /// Determines if the current culture is right-to-left (RTL).
    /// </summary>
    public static bool IsRtl(this CultureInfo cultureInfo) =>
        cultureInfo.Name is "fa-IR" or "ar" or "fa";
}
