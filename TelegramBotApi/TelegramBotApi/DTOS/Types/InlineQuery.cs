using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class InlineQuery
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id;

        [JsonPropertyName("from")]
        [JsonInclude]
        public User From;

        [JsonPropertyName("location")]
        [JsonInclude]
        public Location Location;

        [JsonPropertyName("query")]
        [JsonInclude]
        public string Query;

        [JsonPropertyName("offset")]
        [JsonInclude]
        public string Offset;
    }
}