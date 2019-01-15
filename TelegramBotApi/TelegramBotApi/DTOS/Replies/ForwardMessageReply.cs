using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ForwardMessageReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public Message SentMessage;
    }
}