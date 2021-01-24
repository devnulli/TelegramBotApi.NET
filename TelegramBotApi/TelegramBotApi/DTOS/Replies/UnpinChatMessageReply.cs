using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UnpinChatMessageReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}