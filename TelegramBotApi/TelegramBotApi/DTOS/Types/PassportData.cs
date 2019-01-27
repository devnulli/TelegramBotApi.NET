using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PassportData
    {
        //complete API as of 2019-01-10

        [JsonProperty("data")]
        public EncryptedPassportElement[] EncryptedPassportElements;

        [JsonProperty("credentials")]
        public EncryptedCredentials EncryptedCredentials;
    }
}
