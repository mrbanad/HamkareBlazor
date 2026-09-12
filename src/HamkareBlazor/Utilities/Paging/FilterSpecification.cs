namespace HamkareBlazor;

/// <summary>
/// فیلتر
/// </summary>
public class FilterSpecification
{
    /// <summary>
    /// سازنده
    /// </summary>
    public FilterSpecification(string propertyName)
    {
        PropertyName = propertyName;
    }

    public FilterSpecification(string filterValue, string propertyName)
    {
        FilterValue = filterValue;
        PropertyName = propertyName;
    }

    /// <summary>
    /// از شرط خارج شود
    /// </summary>
    public bool Ignore { get; set; }

    /// <summary>
    /// و / یا
    /// </summary>
    public bool OrBinaryOperation { get; set; }

    /// <summary>
    /// نام خصوصیت برای اعمال فیلتر
    /// </summary>
    public string PropertyName { get; set; }

    /// <summary>
    /// مقدار
    /// </summary>
    public string? FilterValue { get; set; }

    /// <summary>
    /// عملیات
    /// </summary>
    public EOperator FilterOperation { get; set; } = EOperator.Equal;

    /// <summary>
    /// شرط های داخلی
    /// </summary>
    public List<FilterSpecification> FilterSpecifications { get; set; } = [];
}
