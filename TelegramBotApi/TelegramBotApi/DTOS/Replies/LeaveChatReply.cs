using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class LeaveChatReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}