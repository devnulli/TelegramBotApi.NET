using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendLocationReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}