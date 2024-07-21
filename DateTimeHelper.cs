using System.Globalization;

namespace Helpers;

using System.Linq;

/// <summary>
/// DateTime handling tools
/// </summary>
public static class DateTimeHelper
{
    public static DateTime DateTimeMin => new DateTime(1931, 1, 1);


    /// <summary>
    /// return a formatted DateTime (UTC)
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="dateTimeFormat"></param>
    /// <returns></returns>
    public static DateTimeOffset GetFormattedDateTime(string dateTime, string dateTimeFormat)
    {
        if (string.IsNullOrEmpty(dateTime))
        {
            return DateTimeOffset.Now;
        }

        DateTimeOffset formattedDateTime;

        if (string.IsNullOrEmpty(dateTimeFormat))
        {
            formattedDateTime = DateTimeOffset.Parse(dateTime);
        }
        else
        {
            formattedDateTime = DateTimeOffset.ParseExact(dateTime, dateTimeFormat, CultureInfo.InvariantCulture);
        }

        return formattedDateTime;
    }

    public static double GetAgeInYear(DateTime date)
    {
        return (DateTime.Now - date).TotalDays / 365;
    }


    /// <summary>
    /// Get the number of the week
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static int GetWeekNumber(this DateTime date)
    {
        var cultureInfo = CultureInfo.CurrentCulture;
        var calendar = cultureInfo.Calendar;
        var calendarWeekRule = cultureInfo.DateTimeFormat.CalendarWeekRule;
        var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

        return calendar.GetWeekOfYear(date, calendarWeekRule, firstDayOfWeek);

    }


    /// <summary>
    /// Get the first day of the week
    /// depends of the culture
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static DateTime GetFirstDayOfWeek(this DateTime date)
    {
        var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

        while (date.DayOfWeek != firstDayOfWeek)
        {
            date = date.AddDays(-1);
        }

        return date;
    }


    /// <summary>
    /// return collection of date between startDate and endDate with an interval of delta parameter
    /// </summary>
    public static ICollection<DateTimeOffset> GetDatesBetweenPeriod(DateTimeOffset startDate, DateTimeOffset endDate, TimeSpan delta)
    {
        if (startDate > endDate)
        {
            return new List<DateTimeOffset>();
        }

        var dates = new List<DateTimeOffset>();
        while (startDate <= endDate)
        {
            dates.Add(startDate);
            startDate = startDate.Add(delta);
        }

        return dates;
    }


    /// <summary>
    /// Convert a date to a new date for the timeZone parameter
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="timeZone"></param>
    /// <returns></returns>
    public static DateTimeOffset ConvertForTimeZone(this DateTimeOffset dateTime, TimeZoneInfo timeZone)
    {
        return dateTime.ToUniversalTime().ToOffset(timeZone.GetUtcOffset(dateTime));
    }


    /// <summary>
    /// Get the quarter matching to the month
    /// </summary>
    /// <param name="month"></param>
    /// <returns></returns>
    public static int GetQuarter(int month)
    {
        return (int)Math.Ceiling(month / 3.0);
    }


    /// <summary>
    /// return the dates matching to the frequency
    /// </summary>
    /// <param name="dates"></param>
    /// <param name="frequency"></param>
    /// <returns></returns>
    public static IEnumerable<DateTimeOffset> GetDatesInFrequency(this IEnumerable<DateTimeOffset> dates, TimeSpan frequency)
    {
        var result = new List<DateTimeOffset>();
        var goodDate = GetFirstGoodDateInFrequency(dates, frequency);

        if (goodDate == null) return result;
        result.AddRange(from current in dates let delta = (goodDate.Value - current).TotalMinutes where (delta % frequency.TotalMinutes).NearlyZero() select current);

        return result;
    }


    /// <summary>
    /// Return first good date in Frequency
    /// </summary>
    /// <param name="dates"></param>
    /// <param name="frequency"></param>
    /// <returns></returns>
    private static DateTimeOffset? GetFirstGoodDateInFrequency(IEnumerable<DateTimeOffset> dates, TimeSpan frequency)
    {
        DateTimeOffset? goodDate = null;

        if (!dates.Any())
        {
            return null;
        }

        foreach (var current in dates)
        {
            foreach (var nextDate in dates)
            {
                if (nextDate > current && ((nextDate - current).TotalMinutes % frequency.TotalMinutes).NearlyZero())
                {
                    goodDate = current;
                    break;
                }
            }

            if (goodDate.HasValue)
            {
                break;
            }
        }

        return goodDate;
    }


    /// <summary>
    /// get a list of dates separated by one month
    /// </summary>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <returns></returns>
    public static IEnumerable<DateTimeOffset> GetDatesEachMonth(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return new List<DateTimeOffset>();
        }

        var dates = new List<DateTimeOffset>();
        var currentDate = startDate;
        while (currentDate < endDate.AddMonths(1))
        {
            dates.Add(currentDate);
            currentDate = currentDate.AddMonths(1);
        }

        return dates;
    }
}