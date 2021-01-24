using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EditMessageLiveLocationReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}