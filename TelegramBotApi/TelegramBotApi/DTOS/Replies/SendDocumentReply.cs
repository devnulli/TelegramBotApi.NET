using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendDocumentReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
