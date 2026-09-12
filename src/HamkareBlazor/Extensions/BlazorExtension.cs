using System.Security.Claims;
using HamkareBlazor.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace HamkareBlazor;

public static class BlazorExtension
{
    public static async Task LogError(this IJSRuntime runtime, Exception exception)
    {
        await runtime.InvokeVoidAsync("console.error", exception.Message);
        await runtime.InvokeVoidAsync("console.error", exception.StackTrace);
    }

    public static async Task<string?> GetUser(this AuthenticationStateProvider authenticationStateProvider)
    {
        var authState = await authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static FilterViewModel GridStateConvert<T>(this GridState<T> gridState) where T : class
    {
        var result = new FilterViewModel
        {
            PageIndex = gridState.Page,
            PageSize = gridState.PageSize,
            SortItems =
            [
                ..gridState.SortDefinitions.Select(x => new SortItem
                {
                    Direction = x.Descending ? ESortDirection.Descending : ESortDirection.Ascending,
                    SortFieldsSelector = x.SortBy
                }).ToList()
            ],
            FilterSpecifications =
            [
                ..gridState.FilterDefinitions.Where(x => !string.IsNullOrWhiteSpace(x.Column?.PropertyName)).Select(x => new FilterSpecification(x.Column?.PropertyName ?? string.Empty)
                {
                    OrBinaryOperation = false,
                    FilterValue = x.Value?.ToString(),
                    FilterOperation = x.Operator?.ToEnum<EOperator>() ?? EOperator.Equal,
                }).ToList()
            ]
        };

        return result;
    }
}
