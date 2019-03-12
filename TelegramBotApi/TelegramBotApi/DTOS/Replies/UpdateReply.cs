using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UpdateReply : Reply
    {
        [JsonProperty("result")]
        public Update[] Updates;
    }
}
