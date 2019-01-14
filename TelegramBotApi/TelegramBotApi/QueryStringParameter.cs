using System;
using System.Runtime.CompilerServices;

namespace nerderies.TelegramBotApi
{
    public class QueryStringParameter
    {
        public QueryStringParameter(string field, string value)
        {
            if (field == null || value == null)
                throw new ArgumentException("Both parameters must be set");

            Field = field;
            Value = value;
        }
        public string Field { get; private set; }
        public string Value { get; private set; }
    }
}
