using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class StopMessageLiveLocationReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}