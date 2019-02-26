using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class DeleteChatStickerSetReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public bool Success;
    }
}