using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public Chat Chat;
    }
}