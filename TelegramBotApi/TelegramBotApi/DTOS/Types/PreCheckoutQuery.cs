using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PreCheckoutQuery
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id;

        [JsonPropertyName("from")]
        [JsonInclude]
        public User From;

        [JsonPropertyName("currency")]
        [JsonInclude]
        public string Currency;

        [JsonPropertyName("total_amount")]
        [JsonInclude]
        public long TotalAmount;

        [JsonPropertyName("invoice_payload")]
        [JsonInclude]
        public string InvoicePayload;

        [JsonPropertyName("shipping_option_id")]
        [JsonInclude]
        public string ShippingOptionId;

        [JsonPropertyName("order_info")]
        [JsonInclude]
        public string OrderInfo;
    }
}