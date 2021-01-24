using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public abstract class Reply
    {
        [JsonPropertyName("ok")]
        [JsonInclude]
        public bool Ok;
    }
}
