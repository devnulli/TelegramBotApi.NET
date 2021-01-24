using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class OrderInfo
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name;

        [JsonPropertyName("phone_number")]
        [JsonInclude]
        public string PhoneNumber;

        [JsonPropertyName("email")]
        [JsonInclude]
        public string Email;

        [JsonPropertyName("shipping_address")]
        [JsonInclude]
        public ShippingAddress ShippingAddress;
    }
}
