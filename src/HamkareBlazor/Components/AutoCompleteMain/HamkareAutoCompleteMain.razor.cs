using Microsoft.AspNetCore.Components;

namespace HamkareBlazor.Components.AutoCompleteMain;

public partial class HamkareAutoCompleteMain
{
    [EditorRequired] [Parameter] public required string Url { get; set; }
    private List<ISelectItem> _cachedItems = [];

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        SearchFunc = SearchAsync;
        ToStringFunc = ToStringFunction;

        await base.SetParametersAsync(parameters);
    }

    private async Task<IEnumerable<long?>> SearchAsync(string? value, CancellationToken cancellationToken)
    {
        await Task.Delay(250, cancellationToken);

        var result = await ApiService.PostAsync<List<ISelectItem>>(Url,
            new SelectItemSearch
            {
                Search = string.IsNullOrWhiteSpace(value) ? null : value, Page = 1, PageSize = 5
            }) ?? new List<ISelectItem>();

        _cachedItems = result;
        return result.Select<ISelectItem, long?>(x => x.Id);
    }

    private string? ToStringFunction(long? id)
    {
        var item = _cachedItems.FirstOrDefault(x => x.Id == id);
        return item?.Name ?? id.ToString();
    }
}
