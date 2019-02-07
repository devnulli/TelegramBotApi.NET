using System;

namespace nerderies.TelegramBotApi.Converters
{
    public class UnixToDateTimeConverter
    {
        /// <summary>
        /// converts a unix timestamp to an actual datetime
        /// </summary>
        /// <param name="unixDate">the date in seconds since 1970-01-01 00:00</param>
        /// <returns>the converted Datetime</returns>
        public static DateTime Convert(long unixDate)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixDate).ToLocalTime();
            return dateTime;
        }
    }
}
