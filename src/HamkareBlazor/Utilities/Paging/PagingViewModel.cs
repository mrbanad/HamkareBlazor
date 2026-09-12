namespace HamkareBlazor;

public class PagingViewModel<TResult>
{
    public IEnumerable<TResult> Results { get; set; } = [];

    public int Count { get; set; }
}
