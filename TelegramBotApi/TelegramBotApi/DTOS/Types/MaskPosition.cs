using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class MaskPosition
    {
        //complete API as of 2019-01-11

        [JsonProperty("point")]
        public string Point;

        [JsonProperty("x_shift")]
        public double XShift;

        [JsonProperty("y_shift")]
        public double YShift;

        [JsonProperty("scale")]
        public double Float;
    }
}
