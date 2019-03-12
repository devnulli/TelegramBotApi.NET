using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatPhotoReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}