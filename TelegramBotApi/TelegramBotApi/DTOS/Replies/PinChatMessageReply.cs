using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PinChatMessageReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}