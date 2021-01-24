using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Location
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("longitude")]
        [JsonInclude]
        public double Longitude;

        [JsonPropertyName("latitude")]
        [JsonInclude]
        public double Latitude;
    }
}
