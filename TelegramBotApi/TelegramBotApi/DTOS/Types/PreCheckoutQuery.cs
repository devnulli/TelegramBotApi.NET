using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PreCheckoutQuery
    {
        //complete API as of 2019-01-10

        [JsonProperty("id")]
        public string Id;

        [JsonProperty("from")]
        public User From;

        [JsonProperty("currency")]
        public string Currency;

        [JsonProperty("total_amount")]
        public long TotalAmount;

        [JsonProperty("invoice_payload")]
        public string InvoicePayload;

        [JsonProperty("shipping_option_id")]
        public string ShippingOptionId;

        [JsonProperty("order_info")]
        public string OrderInfo;
    }
}