using System;
using System.Collections.Generic;
using System.Text;

namespace MyShop.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime ToTaipeiTime(this DateTime utcTime)
        {
            var tzId = OperatingSystem.IsWindows()
                ? "Taipei Standard Time"
                : "Asia/Taipei";

            var tz = TimeZoneInfo.FindSystemTimeZoneById(tzId);
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, tz);
        }
    }

}
