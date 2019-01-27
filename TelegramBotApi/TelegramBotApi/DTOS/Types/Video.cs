using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Video
    {
        //complete API as of 2019-01-10

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
