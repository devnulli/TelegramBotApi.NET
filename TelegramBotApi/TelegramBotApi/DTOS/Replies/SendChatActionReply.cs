using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendChatActionReply : Reply
    {
        [JsonProperty("result")]
        public bool Result;
    }
}
