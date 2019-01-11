using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Animation
    {
        //complete API as of 2019-01-11

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("width")]
        public long Width;

        [JsonProperty("height")]
        public long Height;

        [JsonProperty("duration")]
        public long Duration;

        [JsonProperty("thumb")]
        public PhotoSize Thumb;

        [JsonProperty("file_name")]
        public string FileName;

        [JsonProperty("mime_type")]
        public string MimeType;

        [JsonProperty("file_size")]
        public string FileSize;
    }
}
