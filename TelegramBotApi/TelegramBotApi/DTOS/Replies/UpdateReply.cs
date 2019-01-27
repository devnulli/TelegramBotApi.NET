using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UpdateReply : Reply
    {
        [JsonProperty("ok")]
        public bool Success;
        [JsonProperty("result")]
        public Update[] Updates;
    }
}
