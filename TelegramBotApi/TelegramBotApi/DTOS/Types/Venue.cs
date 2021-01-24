using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Venue
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("location")]
        [JsonInclude]
        public Location Location;

        [JsonPropertyName("title")]
        [JsonInclude]
        public string Title;

        [JsonPropertyName("address")]
        [JsonInclude]
        public string Address;

        [JsonPropertyName("foursquare_id")]
        [JsonInclude]
        public string FoursquareId;

        [JsonPropertyName("foursquare_type")]
        [JsonInclude]
        public string FoursquareType;
    }
}
