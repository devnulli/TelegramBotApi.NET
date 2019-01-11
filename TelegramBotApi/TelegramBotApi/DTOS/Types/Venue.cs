using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Venue
    {
        //complete API as of 2019-01-11

        [JsonProperty("location")]
        public Location Location;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("address")]
        public string Address;

        [JsonProperty("foursquare_id")]
        public string FoursquareId;

        [JsonProperty("foursquare_type")]
        public string FoursquareType;
    }
}
