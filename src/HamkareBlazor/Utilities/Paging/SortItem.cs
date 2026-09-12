namespace HamkareBlazor;

/// <summary>
/// چیدمان
/// </summary>
public class SortItem
{
    /// <summary>
    /// نوع چیدمان
    /// </summary>
    public ESortDirection Direction { get; set; }

    /// <summary>
    /// نام فیلد اعمال چیدمان
    /// </summary>
    public string? SortFieldsSelector { get; set; }
}
