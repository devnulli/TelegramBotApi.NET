using System;

namespace nerderies.TelegramBotApi
{
    public class MultiPartStringParameter : MultiPartParameter
    {
        public MultiPartStringParameter(string field, string value)
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
