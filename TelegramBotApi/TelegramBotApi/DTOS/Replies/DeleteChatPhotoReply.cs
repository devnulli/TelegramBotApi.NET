using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class DeleteChatPhotoReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}