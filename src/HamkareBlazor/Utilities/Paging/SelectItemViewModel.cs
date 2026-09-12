namespace HamkareBlazor;

public class SelectItemViewModel<TKey>
{
    public SelectItemViewModel(TKey id)
    {
        Id = id;
    }

    public TKey Id { get; set; }

    public string? Name { get; set; }
}

public class SelectItemViewModel
{
    public long Id { get; set; }

    public string? Name { get; set; }
}
