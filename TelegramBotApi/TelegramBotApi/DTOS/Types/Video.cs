using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Video
    {
        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("width")]
        public long Width;

        [JsonProperty("height")]
        public long Height;

        [JsonProperty("duration")]
        public long Duration;

        [JsonProperty("thumb")]
        public PhotoSize ThumbNail;

        [JsonProperty("mime_type")]
        public string MimeType;

        [JsonProperty("file_size")]
        public long FileSize;
    }
}
