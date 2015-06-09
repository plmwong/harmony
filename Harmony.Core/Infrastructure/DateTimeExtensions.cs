using System;

namespace Harmony.Core.Infrastructure
{
    public static class DateTimeExtensions
    {
        public static string ToGeneralFormat(this DateTime dateTime)
        {
            return dateTime.ToString("g");
        }

        public static string ToGoogleFormat(this DateTime dateTime)
        {
            string timezone = TimeZoneInfo.Local.GetUtcOffset(dateTime).ToString();
            if (timezone[0] != '-')
            {
                timezone = '+' + timezone;
            }
            timezone = timezone.Substring(0, 6);

            string result = dateTime.GetDateTimeFormats('s')[0] + timezone;
            return result;
        }

        public static string ToGoogleDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd");
        }

        public static DateTime ToDateTime(this string dateTimeFormatted)
        {
            return DateTime.Parse(dateTimeFormatted);
        }

        public static TimeSpan Weeks(this int weeks)
        {
            return new TimeSpan(7 * weeks, 0, 0, 0);
        }

        public static TimeSpan Days(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        public static TimeSpan Hours(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0);
        }

        public static TimeSpan Minutes(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0);
        }
    }
}

