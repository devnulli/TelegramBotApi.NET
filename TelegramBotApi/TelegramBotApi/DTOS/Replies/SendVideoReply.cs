using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendVideoReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
