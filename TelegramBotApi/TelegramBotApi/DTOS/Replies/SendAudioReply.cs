using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendAudioReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
