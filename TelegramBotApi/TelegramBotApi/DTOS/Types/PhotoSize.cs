using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PhotoSize
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("width")]
        [JsonInclude]
        public long Width;

        [JsonPropertyName("height")]
        [JsonInclude]
        public long Height;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;
    }
}
