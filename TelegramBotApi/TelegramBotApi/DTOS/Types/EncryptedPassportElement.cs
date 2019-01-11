using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EncryptedPassportElement
    {
        //complete API as of 2019-01-11

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("data")]
        public string Data;

        [JsonProperty("phone_number")]
        public string PhoneNumber;

        [JsonProperty("email")]
        public string Email;

        [JsonProperty("files")]
        public PassportFile[] Files;

        [JsonProperty("front_side")]
        public PassportFile FrontSide;

        [JsonProperty("reverse_side")]
        public PassportFile ReverseSide;

        [JsonProperty("selfie")]
        public PassportFile Selfie;

        [JsonProperty("translation")]
        public PassportFile[] Translation;

        [JsonProperty("hash")]
        public string Hash;
}
}
