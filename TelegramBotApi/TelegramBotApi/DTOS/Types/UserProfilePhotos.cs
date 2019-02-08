using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UserProfilePhotos
    {
        //complete API as of 2019-02-08

        [JsonProperty("total_count")]
        public long TotalCount;

        [JsonProperty("photos")]
        public PhotoSize[][] Photos;
    }
}
