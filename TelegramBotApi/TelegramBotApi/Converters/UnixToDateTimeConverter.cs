using System;
using System.Collections.Generic;
using System.Text;

namespace nerderies.TelegramBotApi.Converters
{
    public class UnixToDateTimeConverter
    {
        public static DateTime Convert(long date)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(date).ToLocalTime();
            return dtDateTime;
        }
    }
}
