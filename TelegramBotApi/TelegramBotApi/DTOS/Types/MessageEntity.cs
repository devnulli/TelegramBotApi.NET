using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class MessageEntity
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("type")]
        [JsonInclude]
        public string Type;

        [JsonPropertyName("offset")]
        [JsonInclude]
        public long Offset;

        [JsonPropertyName("length")]
        [JsonInclude]
        public long Length;

        [JsonPropertyName("url")]
        [JsonInclude]
        public string Url;

        [JsonPropertyName("user")]
        [JsonInclude]
        public User User;
    }
}
