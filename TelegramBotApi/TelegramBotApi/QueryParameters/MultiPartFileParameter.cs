using System;

namespace nerderies.TelegramBotApi
{
    public class MultiPartFileParameter : MultiPartParameter
    {
        public MultiPartFileParameter(string field, string filename, byte[] content, string mime)
        {
            if (field == null || filename == null || content == null || mime == null)
                throw new ArgumentNullException("");

            if (content.Length == 0)
                throw new ArgumentException("Content Length cannot be zero.");

            Field = field;
            FileName = filename;
            Content = content;
            Mime = mime;
        }

        public string Field { get; private set; }
        public string FileName { get; private set; }
        public byte[] Content { get; private set; }
        public string Mime { get; private set; }
    }
}
