using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatAdministratorsReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public ChatMember[] Administrators;
    }
}