using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendAudioReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}
