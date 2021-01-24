using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PassportFile
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;

        [JsonPropertyName("file_date")]
        [JsonInclude]
        public long FileDate;
    }
}