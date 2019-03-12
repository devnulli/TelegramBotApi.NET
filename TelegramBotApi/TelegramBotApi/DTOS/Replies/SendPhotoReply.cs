using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendPhotoReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
