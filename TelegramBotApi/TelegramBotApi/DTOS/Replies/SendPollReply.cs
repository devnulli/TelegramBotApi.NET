using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendPollReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
