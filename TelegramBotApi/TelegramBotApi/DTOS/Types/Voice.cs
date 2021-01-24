using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Voice
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("duration")]
        [JsonInclude]
        public long Duration;

        [JsonPropertyName("mime_type")]
        [JsonInclude]
        public string MimeType;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;
    }
}
