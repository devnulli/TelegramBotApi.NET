using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatMembersCountReply : Reply
    {
        [JsonProperty("result")]
        public int ChatMembersCount;
    }
}