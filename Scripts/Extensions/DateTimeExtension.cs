using System;

namespace Sepjani.Helpers.Scripts.Extensions
{
    public static class DateTimeExtension
    {
        public static int ToUnixTimestamp(this DateTime value)
        {
            return (int) Math.Truncate((value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        public static DateTime UnixToDateTime(this DateTime ignore, long unixtime)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixtime).ToLocalTime();
            return dtDateTime;
        }
    }
}