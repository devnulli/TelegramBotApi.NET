using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EncryptedPassportElement
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("type")]
        [JsonInclude]
        public string Type;

        [JsonPropertyName("data")]
        [JsonInclude]
        public string Data;

        [JsonPropertyName("phone_number")]
        [JsonInclude]
        public string PhoneNumber;

        [JsonPropertyName("email")]
        [JsonInclude]
        public string Email;

        [JsonPropertyName("files")]
        [JsonInclude]
        public PassportFile[] Files;

        [JsonPropertyName("front_side")]
        [JsonInclude]
        public PassportFile FrontSide;

        [JsonPropertyName("reverse_side")]
        [JsonInclude]
        public PassportFile ReverseSide;

        [JsonPropertyName("selfie")]
        [JsonInclude]
        public PassportFile Selfie;

        [JsonPropertyName("translation")]
        [JsonInclude]
        public PassportFile[] Translation;

        [JsonPropertyName("hash")]
        [JsonInclude]
        public string Hash;
}
}
