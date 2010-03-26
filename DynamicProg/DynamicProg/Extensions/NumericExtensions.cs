using System;
using System.Text;

namespace DynamicProg.Extensions
{
    public static class NumericExtensions
    {
        public static string AsciiToUnicode(this int asciiCode)
        {
            var ascii = Encoding.UTF32;
            var c = (char)asciiCode;
            var b = ascii.GetBytes(c.ToString());
            return ascii.GetString((b));
        }

        public static bool IsOdd(this int value)
        {
            return ((value & 1) == 1);
        }

        public static bool IsEven(this int value)
        {
            return ((value & 1) == 0);
        }

        public static int PercentOf(this int percentOf, int value)
        {
            return (value*percentOf)/100;               
        }

        public static decimal PercentOf(this int percentOf, decimal value)
        {
            return (value * percentOf) / 100;
        }

        public static int PercentDiscount(this int percentOf, int value)
        {
            return value - percentOf.PercentOf(value);
        }

        public static decimal PercentDiscount(this int percentOf, decimal value)
        {
            return value - percentOf.PercentOf(value);
        }

        public static int PercentSurcharge(this int percentOf, int value)
        {
            return value + percentOf.PercentOf(value);
        }

        public static decimal PercentSurcharge(this int percentOf, decimal value)
        {
            return value + percentOf.PercentOf(value);
        }

        public static DateTime YearsAgo(this int i)
        {
            return DateTime.Now.AddYears(i * -1);
        }

        public static DateTime MonthsAgo(this int i)
        {
            return DateTime.Now.AddMonths(i * -1);
        }

        public static DateTime DaysAgo(this int i)
        {
            return DateTime.Now.AddDays(i*-1);
        }

        public static DateTime HoursAgo(this int i)
        {
            return DateTime.Now.AddHours(i*-1);
        }

        public static DateTime MinutesAgo(this int i)
        {
            return DateTime.Now.AddMinutes(i * -1);
        }

        public static DateTime SecondsAgo(this int i)
        {
            return DateTime.Now.AddSeconds(i * -1);
        }

        public static DateTime YearsFromNow(this int i)
        {
            return DateTime.Now.AddYears(i);
        }

        public static DateTime MonthsFromNow(this int i)
        {
            return DateTime.Now.AddMonths(i);
        }

        public static DateTime DaysFromNow(this int i)
        {
            return DateTime.Now.AddDays(i);
        }

        public static DateTime HoursFromNow(this int i)
        {
            return DateTime.Now.AddHours(i);
        }

        public static DateTime MinutesFromNow(this int i)
        {
            return DateTime.Now.AddMinutes(i);
        }

        public static DateTime SecondsFromNow(this int i)
        {
            return DateTime.Now.AddSeconds(i);
        }

    }
}
