using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendVoiceReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
