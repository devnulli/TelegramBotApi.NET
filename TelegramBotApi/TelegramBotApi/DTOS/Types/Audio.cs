using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS.Types
{
    public class Audio
    {
        //complete API as of 2019-01-10

        [JsonProperty("file_id")]
        public string FileID;

        [JsonProperty("duration")]
        public long Duration;

        [JsonProperty("performer")]
        public long Performer;

        [JsonProperty("title")]
        public long Title;

        [JsonProperty("mime_type")]
        public string MimeType;

        [JsonProperty("file_size")]
        public long FileSize;

        [JsonProperty("thumb")]
        public PhotoSize ThumbNail;
    }
}
