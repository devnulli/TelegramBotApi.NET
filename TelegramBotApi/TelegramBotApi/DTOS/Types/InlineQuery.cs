using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class InlineQuery
    {
        //complete API as of 2019-01-11

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("from")]
        public User From;

        [JsonProperty("location")]
        public Location Location;

        [JsonProperty("query")]
        public string Query;

        [JsonProperty("offset")]
        public string Offset;
    }
}