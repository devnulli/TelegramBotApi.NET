using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SuccessfulPayment
    {
        //complete API as of 2019-01-11

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
        public OrderInfo OrderInfo;

        [JsonPropertyName("telegram_payment_charge_id")]
        [JsonInclude]
        public string TelegramPaymentChargeId;

        [JsonPropertyName("provider_payment_charge_id")]
        [JsonInclude]
        public string ProviderPaymentChargeId;
    }
}
