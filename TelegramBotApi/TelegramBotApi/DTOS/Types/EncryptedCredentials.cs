using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EncryptedCredentials
    {
        //complete API as of 2019-01-11

        [JsonProperty("data")]
        public string Data;

        [JsonProperty("hash")]
        public string Hash;

        [JsonProperty("secret")]
        public string Secret;
    }
}