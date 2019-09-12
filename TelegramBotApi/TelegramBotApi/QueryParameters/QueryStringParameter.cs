using System;

namespace nerderies.TelegramBotApi
{
    public class QueryStringParameter
    {
        public QueryStringParameter(string field, string[] value)
        {
            if (field == null || value == null)
                throw new ArgumentException("Both parameters must be set");
            string[] quoted = new string[value.Length];
            for (int i = 0; i < value.Length; i++)
                quoted[i] = $"\"{value[i]}\"";

            Field = field;
            Value = $"[ {string.Join(", ", quoted)} ]";

        }

        public QueryStringParameter(string field, string value)
        {
            if (field == null || value == null)
                throw new ArgumentException("Both parameters must be set");

            Field = field;
            Value = value;
        }
        public string Field { get; private set; }
        public string Value { get; internal set; }
    }
}
