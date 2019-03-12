using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatTitleReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}