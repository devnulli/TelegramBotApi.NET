using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetUserProfilePhotosReply : Reply
    {
        [JsonProperty("result")]
        public UserProfilePhotos UserProfilePhotos;
    }
}
