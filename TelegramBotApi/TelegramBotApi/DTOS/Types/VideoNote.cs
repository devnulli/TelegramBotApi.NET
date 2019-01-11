using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class VideoNote
    {
        //complete API as of 2019-01-011

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("length")]
        public long Length;

        [JsonProperty("duration")]
        public long Duration;

        [JsonProperty("thumb")]
        public PhotoSize ThumbNail;

        [JsonProperty("file_size")]
        public long FileSize;
    }
}
