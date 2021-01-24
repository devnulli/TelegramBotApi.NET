using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class User
    {
        //complete API of 2019-01-10

        [JsonPropertyName("id")]
        [JsonInclude]
        public long Id;

        [JsonPropertyName("is_bot")]
        [JsonInclude]
        public bool IsBot;

        [JsonPropertyName("first_name")]
        [JsonInclude]
        public string FirstName;

        [JsonPropertyName("last_name")]
        [JsonInclude]
        public string LastName;

        [JsonPropertyName("username")]
        [JsonInclude]
        public string Username;

        [JsonPropertyName("language_code")]
        [JsonInclude]
        public string LanguageCode;
    }
}
