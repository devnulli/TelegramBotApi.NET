using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ShippingQuery
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("from")]
        public User From;

        [JsonProperty("invoice_payload")]
        public string InvoicePayload;

        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress;
    }
}