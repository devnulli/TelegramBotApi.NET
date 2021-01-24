using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ForwardMessageReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}