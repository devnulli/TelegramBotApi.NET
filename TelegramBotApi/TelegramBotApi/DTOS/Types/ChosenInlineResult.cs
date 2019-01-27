using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ChosenInlineResult
    {
        //complete API as of 2019-01-10

        [JsonProperty("result_id")]
        public string ResultId;

        [JsonProperty("from")]
        public User From;

        [JsonProperty("location")]
        public Location Location;

        [JsonProperty("inline_message_id")]
        public string InlineMessageId;

        [JsonProperty("query")]
        public string Query;
    }
}