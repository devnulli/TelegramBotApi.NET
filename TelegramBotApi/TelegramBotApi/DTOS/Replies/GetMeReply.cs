using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetMeReply : Reply
    {
        [JsonProperty("ok")]
        public bool Success;

        [JsonProperty("result")]
        public User Result;
    }
}
