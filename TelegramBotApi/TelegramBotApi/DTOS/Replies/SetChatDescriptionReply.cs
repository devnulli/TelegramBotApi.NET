using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatDescriptionReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}