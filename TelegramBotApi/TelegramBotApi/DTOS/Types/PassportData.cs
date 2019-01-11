using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PassportData
    {
        [JsonProperty("data")]
        public EncryptedPassportElement[] EncryptedPassportElements;

        [JsonProperty("credentials")]
        public EncryptedCredentials EncryptedCredentials;
    }
}
