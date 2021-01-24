using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatMembersCountReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public int ChatMembersCount;
    }
}