using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatMemberReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public ChatMember Result;
    }
}