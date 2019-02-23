using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatAdministratorsReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public ChatMember[] Administrators;
    }
}