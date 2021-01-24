using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatPhotoReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}