using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Document
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("thumb")]
        [JsonInclude]
        public PhotoSize Thumb;

        [JsonPropertyName("file_name")]
        [JsonInclude]
        public string FileName;

        [JsonPropertyName("mime_type")]
        [JsonInclude]
        public string MimeType;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;
    }
}
