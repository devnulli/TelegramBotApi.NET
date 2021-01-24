using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Chat Chat;
    }
}