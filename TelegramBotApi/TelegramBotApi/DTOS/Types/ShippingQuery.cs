using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ShippingQuery
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id;

        [JsonPropertyName("from")]
        [JsonInclude]
        public User From;

        [JsonPropertyName("invoice_payload")]
        [JsonInclude]
        public string InvoicePayload;

        [JsonPropertyName("shipping_address")]
        [JsonInclude]
        public ShippingAddress ShippingAddress;
    }
}