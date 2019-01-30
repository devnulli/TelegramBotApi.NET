using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Location
    {
        //complete API as of 2019-01-11

        [JsonProperty("longitude")]
        public double Longitude;

        [JsonProperty("latitude")]
        public double Latitude;
    }
}
