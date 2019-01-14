using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetMeReply : Reply
    {
        //complete API as of 2019-01-10

        [JsonProperty("ok")]
        public bool Success;
        [JsonProperty("result")]
        public User Result;
    }
}
