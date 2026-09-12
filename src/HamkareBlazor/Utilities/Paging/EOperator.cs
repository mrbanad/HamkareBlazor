namespace HamkareBlazor;

/// <summary>
/// عملیات فیلتر
/// </summary>
public enum EOperator
{
    /// <summary>
    /// برابری
    /// </summary>
    Equal = 1,

    /// <summary>
    /// مشابه
    /// </summary>
    Like = 2,

    /// <summary>
    /// نا برابر
    /// </summary>
    NotEqual = 3,

    /// <summary>
    /// بزرگتر از
    /// </summary>
    GreaterThan = 4,

    /// <summary>
    /// بزرگتر مساوی
    /// </summary>
    GreaterThanOrEqual = 5,

    /// <summary>
    /// کوچکتر از
    /// </summary>
    LessThan = 6,

    /// <summary>
    /// کوچکتر مساوی
    /// </summary>
    LessThanOrEqual = 7,

    /// <summary>
    /// مابین
    /// </summary>
    Between = 8,

    /// <summary>
    /// شروع با
    /// </summary>
    StartsWith,

    /// <summary>
    /// پایان با
    /// </summary>
    EndsWith,

    /// <summary>
    /// نال بودن
    /// </summary>
    IsNull = 11
}
