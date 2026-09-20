using System.Globalization;

namespace HamkareBlazor;

/// <summary>
/// Extension methods for <see cref="DateTime"/>, <see cref="DateOnly"/> and <see cref="TimeSpan"/> for
/// conversions between Gregorian and Persian calendars, rounding, Unix timestamp, and formatted strings.
/// </summary>
public static class DateExtension
{
    /// <summary>
    /// Converts a Gregorian date to a Persian date string (yyyy-MM-dd).
    /// </summary>
    public static string GregorianToPersian(this DateTime dateTime)
    {
        var pc = new PersianCalendar();

        return $"{pc.GetYear(dateTime)}-{pc.GetMonth(dateTime)}-{pc.GetDayOfMonth(dateTime)}";
    }

    /// <summary>
    /// Converts a Persian date string to a Gregorian <see cref="DateTime"/>.
    /// </summary>
    public static DateTime PersianToGregorian(this string dateTimeString)
    {
        var datetime = DateTime.Parse(dateTimeString);

        return new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, datetime.Minute,
            datetime.Second, new PersianCalendar());
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> to Unix timestamp (seconds since 1970-01-01 UTC).
    /// </summary>
    public static long ConvertToUnix(this DateTime dateTime)
    {
        return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
    }

    /// <summary>
    /// Rounds up a <see cref="DateTime"/> to the nearest quarter-hour.
    /// </summary>
    public static DateTime RoundUp(this DateTime dateTime)
    {
        var minute = dateTime.Minute;

        var retDateTime = (minute / 15) switch
        {
            0 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0),
            1 or 2 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 30, 0),
            3 => new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0).AddHours(1),
            _ => dateTime
        };

        return retDateTime;
    }

    /// <summary>
    /// Rounds up a <see cref="TimeSpan"/> to the nearest quarter-hour.
    /// </summary>
    public static TimeSpan RoundUp(this TimeSpan dateTime)
    {
        var minute = dateTime.Minutes;

        var retDateTime = (minute / 15) switch
        {
            0 => new TimeSpan(dateTime.Days, dateTime.Hours, 0, 0),
            1 or 2 => new TimeSpan(dateTime.Days, dateTime.Hours, 30, 0),
            3 => new TimeSpan(dateTime.Days, dateTime.Hours + 1, 0, 0),
            _ => dateTime
        };

        return retDateTime;
    }

    /// <summary>
    /// Formats a <see cref="DateTime"/> as a string, optionally including the time, based on the current UI culture.
    /// </summary>
    public static string ToString(this DateTime dateTime, bool time)
    {
        return time
            ? dateTime.ToString(
                Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd HH:mm:ss" : "MM/dd/yyyy HH:mm:ss",
                Thread.CurrentThread.CurrentUICulture)
            : dateTime.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy",
                Thread.CurrentThread.CurrentUICulture);
    }

    /// <summary>
    /// Formats a nullable <see cref="DateTime"/> as a string, optionally including the time, based on the current UI culture.
    /// </summary>
    public static string ToString(this DateTime? dateTime, bool time)
    {
        return dateTime.HasValue
            ? time
                ? dateTime.Value.ToString(
                    Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd HH:mm:ss" : "MM/dd/yyyy HH:mm:ss",
                    Thread.CurrentThread.CurrentUICulture)
                : dateTime.Value.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy",
                    Thread.CurrentThread.CurrentUICulture)
            : string.Empty;
    }


    /// <summary>
    /// Formats a <see cref="DateOnly"/> as a string based on the current UI culture.
    /// </summary>
    public static string ToString(this DateOnly dateTime)
    {
        return dateTime.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy",
            Thread.CurrentThread.CurrentUICulture);
    }

    /// <summary>
    /// Formats a nullable <see cref="DateOnly"/> as a string based on the current UI culture.
    /// </summary>
    public static string ToString(this DateOnly? dateTime)
    {
        return dateTime.HasValue
            ? dateTime.Value.ToString(Thread.CurrentThread.CurrentUICulture.IsRtl() ? "yyyy/MM/dd" : "MM/dd/yyyy",
                Thread.CurrentThread.CurrentUICulture)
            : string.Empty;
    }
}
