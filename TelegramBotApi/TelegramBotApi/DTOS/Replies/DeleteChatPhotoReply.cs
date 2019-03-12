using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class DeleteChatPhotoReply : Reply
    {
        [JsonProperty("result")]
        public bool Success;
    }
}