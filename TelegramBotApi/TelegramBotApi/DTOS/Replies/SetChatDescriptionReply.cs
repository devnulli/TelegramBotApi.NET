using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatDescriptionReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}