using System;

namespace DynamicProg.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns a double that represents the Unix Time for a given date.
        /// </summary>
        public static double ToUnixTime(this DateTime d)
        {
            try
            {
                return (d.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            }
            catch
            {
                return 0;
            }
        }

        public static DateTime FromUnixTime(this double unixDate)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(unixDate);
        }

        public static bool IsWeekDay(this DateTime dt)
        {
            return Convert.ToInt16(dt.DayOfWeek) < 6;
        }

        public static bool IsWeekEnd(this DateTime dt)
        {
            return Convert.ToInt16(dt.DayOfWeek) > 5;
        }

        public static DateTime Yesterday(this DateTime dt)
        {
            return DateTime.Today.AddDays(1);
        }

        public static DateTime Tomorrow(this DateTime dt)
        {
            return DateTime.Today.AddDays(1);
        }
    }
}
