using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class StopMessageLiveLocationReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}