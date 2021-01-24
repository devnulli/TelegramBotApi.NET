using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendMessageReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}
