using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UserProfilePhotos
    {
        //complete API as of 2019-02-08

        [JsonPropertyName("total_count")]
        [JsonInclude]
        public long TotalCount;

        [JsonPropertyName("photos")]
        [JsonInclude]
        public PhotoSize[][] Photos;
    }
}
