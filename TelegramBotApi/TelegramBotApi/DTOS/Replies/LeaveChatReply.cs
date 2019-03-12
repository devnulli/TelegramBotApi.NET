using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class LeaveChatReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}