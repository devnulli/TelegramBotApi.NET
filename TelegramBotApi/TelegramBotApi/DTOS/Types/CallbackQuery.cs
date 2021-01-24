using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class CallbackQuery
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id;

        [JsonPropertyName("from")]
        [JsonInclude]
        public User From;

        [JsonPropertyName("message")]
        [JsonInclude]
        public Message Message;

        [JsonPropertyName("inline_message_id")]
        [JsonInclude]
        public string InlineMessageId;

        [JsonPropertyName("chat_instance")]
        [JsonInclude]
        public string ChatInstance;

        [JsonPropertyName("data")]
        [JsonInclude]
        public string Data;

        [JsonPropertyName("game_short_name")]
        [JsonInclude]
        public string GameShortName;
    }
}