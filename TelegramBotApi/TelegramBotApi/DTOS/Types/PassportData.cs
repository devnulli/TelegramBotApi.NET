using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PassportData
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("data")]
        [JsonInclude]
        public EncryptedPassportElement[] EncryptedPassportElements;

        [JsonPropertyName("credentials")]
        [JsonInclude]
        public EncryptedCredentials EncryptedCredentials;
    }
}
