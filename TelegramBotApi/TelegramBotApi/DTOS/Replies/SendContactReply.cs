using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendContactReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}