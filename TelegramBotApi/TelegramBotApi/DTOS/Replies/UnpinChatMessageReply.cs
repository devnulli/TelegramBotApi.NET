using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UnpinChatMessageReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}