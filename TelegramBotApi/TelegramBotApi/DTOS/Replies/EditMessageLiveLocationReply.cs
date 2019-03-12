using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EditMessageLiveLocationReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}