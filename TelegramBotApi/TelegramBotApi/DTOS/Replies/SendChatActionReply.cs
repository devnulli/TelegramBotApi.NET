using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendChatActionReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public bool Result;
    }
}
