using System.Globalization;
using System.Reflection;

namespace HamkareBlazor;

public static class CultureExtension
{
    private static CultureInfo? _culture;

    public static CultureInfo GetPersianCulture()
    {
        if (_culture != null)
            return _culture;

        _culture = new CultureInfo("fa-IR");
        var formatInfo = _culture.DateTimeFormat;
        formatInfo.MonthNames =
        [
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند",
            ""
        ];
        formatInfo.MonthGenitiveNames =
        [
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند",
            ""
        ];
        formatInfo.AbbreviatedMonthNames =
        [
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند",
            ""
        ];
        formatInfo.AbbreviatedMonthGenitiveNames =
        [
            "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند",
            ""
        ];
        formatInfo.AbbreviatedDayNames = ["ی", "د", "س", "چ", "پ", "ج", "ش"];
        formatInfo.ShortestDayNames = ["ی", "د", "س", "چ", "پ", "ج", "ش"];
        formatInfo.DayNames = ["یکشنبه", "دوشنبه", "ﺳﻪشنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه"];
        formatInfo.AMDesignator = "ق.ظ";
        formatInfo.PMDesignator = "ب.ظ";
        formatInfo.ShortTimePattern = "HH:mm";

        formatInfo.DateSeparator = "/";

        formatInfo.FullDateTimePattern = "dd/MM/yyyy HH:mm";

        formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
        formatInfo.ShortDatePattern = "dd/MM/yyyy";

        formatInfo.LongDatePattern = "dd/MM/yyyy HH:mm";
        formatInfo.SetAllDateTimePatterns(["dd/MM/yyyy"], 'd');
        formatInfo.SetAllDateTimePatterns(["dddd, dd MMMM yyyy"], 'D');

        formatInfo.SetAllDateTimePatterns(["yyyy MMMM"], 'y');
        formatInfo.SetAllDateTimePatterns(["yyyy MMMM"], 'Y');

        formatInfo.FirstDayOfWeek = DayOfWeek.Saturday;
        Calendar cal = new PersianCalendar();

        var fieldInfo =
            _culture.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (fieldInfo != null)
            fieldInfo.SetValue(_culture, cal);

        var info = formatInfo.GetType().GetField("calendar", BindingFlags.NonPublic | BindingFlags.Instance);
        if (info != null)
            info.SetValue(formatInfo, cal);

        _culture.NumberFormat.NumberDecimalSeparator = "٫";
        _culture.NumberFormat.DigitSubstitution = DigitShapes.NativeNational;
        _culture.NumberFormat.NumberNegativePattern = 0;

        return _culture;
    }

    public static string ToPersianDateString(this DateTime date, string format = "yyyy/MM/dd")
    {
        return date.ToString(format, GetPersianCulture());
    }
}
