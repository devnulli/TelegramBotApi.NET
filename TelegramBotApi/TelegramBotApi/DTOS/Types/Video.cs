using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Video
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("width")]
        [JsonInclude]
        public long Width;

        [JsonPropertyName("height")]
        [JsonInclude]
        public long Height;

        [JsonPropertyName("duration")]
        [JsonInclude]
        public long Duration;

        [JsonPropertyName("thumb")]
        [JsonInclude]
        public PhotoSize ThumbNail;

        [JsonPropertyName("mime_type")]
        [JsonInclude]
        public string MimeType;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;
    }
}
