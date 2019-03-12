using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatAdministratorsReply : Reply
    {
        [JsonProperty("result")]
        public ChatMember[] Administrators;
    }
}