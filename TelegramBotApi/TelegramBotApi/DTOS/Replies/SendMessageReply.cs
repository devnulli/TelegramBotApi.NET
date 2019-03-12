using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendMessageReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
