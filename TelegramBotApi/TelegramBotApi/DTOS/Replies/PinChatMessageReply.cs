using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PinChatMessageReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}