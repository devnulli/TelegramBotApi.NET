using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class CallbackQuery
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("from")]
        public User From;

        [JsonProperty("message")]
        public Message Message;

        [JsonProperty("inline_message_id")]
        public string InlineMessageId;

        [JsonProperty("chat_instance")]
        public string ChatInstance;

        [JsonProperty("data")]
        public string Data;

        [JsonProperty("game_short_name")]
        public string GameShortName;
    }
}