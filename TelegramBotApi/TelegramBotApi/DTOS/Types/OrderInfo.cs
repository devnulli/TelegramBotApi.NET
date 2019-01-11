using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class OrderInfo
    {
        //complete API as of 2019-01-11

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("phone_number")]
        public string PhoneNumber;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("shipping_address")]
        public ShippingAddress ShippingAddress;
    }
}
