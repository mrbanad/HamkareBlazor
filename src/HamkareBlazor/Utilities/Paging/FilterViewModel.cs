using HamkareBlazor.Extensions;

namespace HamkareBlazor;

/// <summary>
/// مدل اعمال فیلتر برای داده ها
/// </summary>
[Serializable]
public class FilterViewModel
{
    /// <summary>
    /// شمارشی شناسایی صفحه
    /// </summary>
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// تعداد رکورد در دیتا
    /// </summary>
    public int PageSize { get; set; } = 5;

    public List<SortItem> SortItems { get; set; } = [];

    ///// <summary>
    ///// دریافت چیدمان
    ///// </summary>
    ///// <returns></returns>
    //public List<SortItem> SortItems() =>
    //    SortSpecification;
    /// <summary>
    /// و / یا
    /// </summary>
    public bool? OrBinaryOperation { get; set; } = null;

    /// <summary>
    /// زبان
    /// </summary>
    public Language Language => Thread.CurrentThread.CurrentUICulture.Name.ToEnum<Language>();

    public List<FilterSpecification>? FilterSpecifications { get; set; } = [];
}
