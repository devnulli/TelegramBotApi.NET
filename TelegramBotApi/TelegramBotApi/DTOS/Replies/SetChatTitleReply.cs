using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatTitleReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}