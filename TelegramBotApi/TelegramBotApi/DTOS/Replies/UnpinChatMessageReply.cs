using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UnpinChatMessageReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public bool Success;
    }
}