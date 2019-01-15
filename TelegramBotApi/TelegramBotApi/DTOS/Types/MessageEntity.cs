using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class MessageEntity
    {
        //complete API as of 2019-01-10

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("offset")]
        public long Offset;

        [JsonProperty("length")]
        public long Length;

        [JsonProperty("url")]
        public string Url;

        [JsonProperty("user")]
        public User User;
    }
}
