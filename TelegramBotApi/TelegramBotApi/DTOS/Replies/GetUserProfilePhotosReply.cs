using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetUserProfilePhotosReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public UserProfilePhotos UserProfilePhotos;
    }
}
