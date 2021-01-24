using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UpdateReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Update[] Updates;
    }
}
