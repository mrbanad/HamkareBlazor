// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace HamkareBlazor.Extensions
{
    public static class DoubleExtensions
    {
        public static string ToInvariantString(this double input) => input.ToString(CultureInfo.InvariantCulture);
    }
}
