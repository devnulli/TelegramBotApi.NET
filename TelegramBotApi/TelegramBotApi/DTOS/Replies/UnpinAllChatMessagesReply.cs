using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UnpinAllChatMessagesReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}