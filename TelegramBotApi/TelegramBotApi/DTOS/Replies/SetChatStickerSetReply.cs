using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatStickerSetReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Success;
    }
}