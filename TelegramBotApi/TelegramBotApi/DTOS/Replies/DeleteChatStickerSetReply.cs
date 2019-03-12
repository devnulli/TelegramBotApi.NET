using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class DeleteChatStickerSetReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}