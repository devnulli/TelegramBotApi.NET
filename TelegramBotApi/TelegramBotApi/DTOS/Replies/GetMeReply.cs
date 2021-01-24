using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetMeReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public User Result;
    }
}
