// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace HamkareBlazor;

#nullable enable
internal partial class DefaultConverter
{
    internal sealed class StringConverter : IReversibleConverter<string?, string?>
    {
        public string? Convert(string? input) => input;

        public string? ConvertBack(string? input) => input;

        public static readonly StringConverter Instance = new();
    }
}
