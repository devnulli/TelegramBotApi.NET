using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Invoice
    {
        //complete API as of 2019-01-11

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("start_parameter")]
        public string StartParameter;

        [JsonProperty("currency")]
        public string Currency;

        [JsonProperty("total_amount")]
        public long TotalAmount;
    }
}
