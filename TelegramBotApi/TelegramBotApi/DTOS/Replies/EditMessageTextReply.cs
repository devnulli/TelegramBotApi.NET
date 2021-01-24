using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EditMessageTextReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message UpdatedMessage;
    }
}
