using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendChatActionReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public bool Result;
    }
}
