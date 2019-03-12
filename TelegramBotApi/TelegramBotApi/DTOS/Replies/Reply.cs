using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public abstract class Reply
    {
        [JsonProperty("ok")]
        public bool Ok;
    }
}
