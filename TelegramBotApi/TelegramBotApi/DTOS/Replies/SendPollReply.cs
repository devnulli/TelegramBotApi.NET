using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendPollReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}
