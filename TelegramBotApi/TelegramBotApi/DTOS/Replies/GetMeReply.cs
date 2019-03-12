using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetMeReply : Reply
    {
        [JsonProperty("result")]
        public User Result;
    }
}
