using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ShippingAddress
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("country_code")]
        [JsonInclude]
        public string CountryCode;

        [JsonPropertyName("state")]
        [JsonInclude]
        public string State;

        [JsonPropertyName("city")]
        [JsonInclude]
        public string City;

        [JsonPropertyName("street_line1")]
        [JsonInclude]
        public string StreetLine1;

        [JsonPropertyName("street_line2")]
        [JsonInclude]
        public string StreetLine2;

        [JsonPropertyName("post_code")]
        [JsonInclude]
        public string PostCode;
    }
}
