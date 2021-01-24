using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Audio
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("file_id")]
        public string FileID;

        [JsonPropertyName("duration")]
        public long Duration;

        [JsonPropertyName("performer")]
        public string Performer;

        [JsonPropertyName("title")]
        public string Title;

        [JsonPropertyName("mime_type")]
        public string MimeType;

        [JsonPropertyName("file_size")]
        public long FileSize;

        [JsonPropertyName("thumb")]
        public PhotoSize ThumbNail;
    }
}
