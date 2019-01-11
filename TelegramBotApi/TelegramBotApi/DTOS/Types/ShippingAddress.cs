using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ShippingAddress
    {
        //complete API as of 2019-01-11

        [JsonProperty("country_code")]
        public string CountryCode;

        [JsonProperty("state")]
        public string State;

        [JsonProperty("city")]
        public string City;

        [JsonProperty("street_line1")]
        public string StreetLine1;

        [JsonProperty("street_line2")]
        public string StreetLine2;

        [JsonProperty("post_code")]
        public string PostCode;
    }
}
