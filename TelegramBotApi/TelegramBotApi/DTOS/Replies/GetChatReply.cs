using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatReply : Reply
    {
        [JsonProperty("result")]
        public Chat Chat;
    }
}