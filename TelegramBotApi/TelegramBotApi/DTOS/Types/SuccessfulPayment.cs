using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SuccessfulPayment
    {
        //complete API as of 2019-01-11

        [JsonProperty("currency")]
        public string Currency;

        [JsonProperty("total_amount")]
        public long TotalAmount;

        [JsonProperty("invoice_payload")]
        public string InvoicePayload;

        [JsonProperty("shipping_option_id")]
        public string ShippingOptionId;

        [JsonProperty("order_info")]
        public OrderInfo OrderInfo;

        [JsonProperty("telegram_payment_charge_id")]
        public string TelegramPaymentChargeId;

        [JsonProperty("provider_payment_charge_id")]
        public string ProviderPaymentChargeId;
    }
}
