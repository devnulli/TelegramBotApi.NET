using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Contact
    {
        //complete API as of 2019-01-11

        [JsonProperty("phone_number")]
        public string PhoneNumber;

        [JsonProperty("first_name")]
        public string FirstName;

        [JsonProperty("last_name")]
        public string LastName;

        [JsonProperty("user_id")]
        public long UserId;

        [JsonProperty("vcard")]
        public string VCard;
    }
}
