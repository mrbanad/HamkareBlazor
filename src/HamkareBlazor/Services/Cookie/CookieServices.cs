using Microsoft.JSInterop;

namespace HamkareBlazor;

public sealed class CookieService(IJSRuntime jsRuntime)
{
    public async Task<string?> GetAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return await jsRuntime.InvokeAsync<string?>(
            "cookieHelper.get",
            name);
    }

    public async Task SetAsync(
        string name,
        string value,
        int? days = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        await jsRuntime.InvokeVoidAsync(
            "cookieHelper.set",
            name,
            value,
            days);
    }

    public async Task DeleteAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        await jsRuntime.InvokeVoidAsync(
            "cookieHelper.delete",
            name);
    }

    public static string? ParseAspNetCoreCultureCookie(
        string? cookieValue)
    {
        if (string.IsNullOrWhiteSpace(cookieValue))
            return null;

        var decodedValue = Uri.UnescapeDataString(cookieValue);

        foreach (var part in decodedValue.Split('|'))
        {
            var separatorIndex = part.IndexOf('=');

            if (separatorIndex <= 0)
                continue;

            var key = part[..separatorIndex];
            var value = part[(separatorIndex + 1)..];

            if (key.Equals("uic", StringComparison.Ordinal))
                return value;
        }

        return null;
    }
}
