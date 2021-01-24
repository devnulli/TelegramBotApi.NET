using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EncryptedCredentials
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("data")]
        [JsonInclude]
        public string Data;

        [JsonPropertyName("hash")]
        [JsonInclude]
        public string Hash;

        [JsonPropertyName("secret")]
        [JsonInclude]
        public string Secret;
    }
}