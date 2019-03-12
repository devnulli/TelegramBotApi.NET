using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ForwardMessageReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}