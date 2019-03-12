using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendVenueReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}