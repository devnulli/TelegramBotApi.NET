using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ChatPhoto
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("small_file_id")]
        [JsonInclude]
        public string SmallFileId;

        [JsonPropertyName("big_file_id")]
        [JsonInclude]
        public string BigFileId;
    }
}
