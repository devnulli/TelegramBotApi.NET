using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class VideoNote
    {
        //complete API as of 2019-01-011

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("length")]
        [JsonInclude]
        public long Length;

        [JsonPropertyName("duration")]
        [JsonInclude]
        public long Duration;

        [JsonPropertyName("thumb")]
        [JsonInclude]
        public PhotoSize ThumbNail;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;
    }
}
