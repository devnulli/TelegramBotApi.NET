using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ChosenInlineResult
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("result_id")]
        [JsonInclude]
        public string ResultId;

        [JsonPropertyName("from")]
        [JsonInclude]
        public User From;

        [JsonPropertyName("location")]
        [JsonInclude]
        public Location Location;

        [JsonPropertyName("inline_message_id")]
        [JsonInclude]
        public string InlineMessageId;

        [JsonPropertyName("query")]
        [JsonInclude]
        public string Query;
    }
}