using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class DeleteChatStickerSetReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}