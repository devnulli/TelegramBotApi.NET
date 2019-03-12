using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendAnimationReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
