using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class User
    {
        //complete API of 2019-01-10

        [JsonProperty("id")]
        public long Id;

        [JsonProperty("is_bot")]
        public bool IsBot;

        [JsonProperty("first_name")]
        public string FirstName;

        [JsonProperty("last_name")]
        public string LastName;

        [JsonProperty("username")]
        public string Username;

        [JsonProperty("language_code")]
        public string LanguageCode;
    }
}
