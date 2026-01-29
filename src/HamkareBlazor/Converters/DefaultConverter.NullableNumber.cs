// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Numerics;
using HamkareBlazor.Resources;
using HamkareBlazor.Utilities.Exceptions;

namespace HamkareBlazor;

#nullable enable
internal partial class DefaultConverter
{
    internal sealed class NullableNumberConverter<TNumber>(Func<CultureInfo> culture, Func<string?> format)
        : IReversibleConverter<TNumber?, string?>
        where TNumber : struct, INumber<TNumber>
    {
        public string? Convert(TNumber? input) => input?.ToString(format.Invoke(), culture.Invoke());

        public TNumber? ConvertBack(string? input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var currentCulture = culture.Invoke();

            if (TNumber.TryParse(input, NumberStyles.Any, currentCulture, out var result))
            {
                return result;
            }

            throw new ConversionException(LanguageResource.Converter_InvalidNumber);
        }
    }
}
