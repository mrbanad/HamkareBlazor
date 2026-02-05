// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace HamkareBlazor.Extensions;

public static class LanguageExtensions
{
    /// <summary>
    /// Maps the current culture to a two-letter country code.
    /// </summary>
    public static string GetCountry(this CultureInfo cultureInfo) => cultureInfo.Name switch
    {
        "fa" or "fa-IR" => "ir",
        "en" or "en-US" => "us",
        "tr" or "tr-TR" => "tr",
        "ar" or "ar-IQ" => "iq",
        _ => "un"
    };

    /// <summary>
    /// Returns a dictionary of other supported languages, excluding the current culture.
    /// </summary>
    public static Dictionary<string, string> OtherSupportedLanguage(this CultureInfo cultureInfo)
    {
        var allSupported = AllSupportedLanguage();
        allSupported.Remove(cultureInfo.Name);
        return allSupported;
    }

    /// <summary>
    /// Returns all supported languages with their corresponding country codes.
    /// </summary>
    private static Dictionary<string, string> AllSupportedLanguage() => new()
    {
        ["fa-IR"] = "ir", ["en"] = "us", ["ar"] = "iq", ["tr"] = "tr"
    };

    /// <summary>
    /// Returns the query string for UI culture in URLs. Empty for default Persian culture.
    /// </summary>
    public static string GetUrlCulture(this CultureInfo cultureInfo) =>
        cultureInfo.Name == "fa-IR" ? "" : $"?ui-culture={cultureInfo.Name}";

    /// <summary>
    /// Determines if the current culture is right-to-left (RTL).
    /// </summary>
    public static bool IsRtl(this CultureInfo cultureInfo) =>
        cultureInfo.Name is "fa-IR" or "ar" or "fa";
}
