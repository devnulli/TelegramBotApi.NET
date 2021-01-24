using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendContactReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}