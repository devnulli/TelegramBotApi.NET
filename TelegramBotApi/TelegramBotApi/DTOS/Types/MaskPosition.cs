using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class MaskPosition
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("point")]
        [JsonInclude]
        public string Point;

        [JsonPropertyName("x_shift")]
        [JsonInclude]
        public double XShift;

        [JsonPropertyName("y_shift")]
        [JsonInclude]
        public double YShift;

        [JsonPropertyName("scale")]
        [JsonInclude]
        public double Float;
    }
}
