using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetUserProfilePhotosReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public UserProfilePhotos UserProfilePhotos;
    }
}
