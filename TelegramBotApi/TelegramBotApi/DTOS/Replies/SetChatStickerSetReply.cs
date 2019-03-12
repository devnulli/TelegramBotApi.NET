using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatStickerSetReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}