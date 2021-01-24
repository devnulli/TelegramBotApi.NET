using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Invoice
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("title")]
        [JsonInclude]
        public string Title;

        [JsonPropertyName("description")]
        [JsonInclude]
        public string Description;

        [JsonPropertyName("start_parameter")]
        [JsonInclude]
        public string StartParameter;

        [JsonPropertyName("currency")]
        [JsonInclude]
        public string Currency;

        [JsonPropertyName("total_amount")]
        [JsonInclude]
        public long TotalAmount;
    }
}
