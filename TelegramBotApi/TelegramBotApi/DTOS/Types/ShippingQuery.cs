using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ShippingQuery
    {
        //complete API as of 2019-01-10

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