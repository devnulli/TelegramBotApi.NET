using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Contact
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("phone_number")]
        [JsonInclude]
        public string PhoneNumber;

        [JsonPropertyName("first_name")]
        [JsonInclude]
        public string FirstName;

        [JsonPropertyName("last_name")]
        [JsonInclude]
        public string LastName;

        [JsonPropertyName("user_id")]
        [JsonInclude]
        public long UserId;

        [JsonPropertyName("vcard")]
        [JsonInclude]
        public string VCard;
    }
}
