using System;

namespace App.Common.Extensions
{
    public static class AppDateTimeExtensions
    {
        public static DateTime CentralStandardTime(this DateTime date)
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time (Mexico)");
            return TimeZoneInfo.ConvertTimeFromUtc(date, timeZoneInfo);
        }
    }
}
