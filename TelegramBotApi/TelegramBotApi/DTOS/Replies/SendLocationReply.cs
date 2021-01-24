using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendLocationReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}