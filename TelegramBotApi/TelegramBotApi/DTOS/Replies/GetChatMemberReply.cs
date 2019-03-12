using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetChatMemberReply : Reply
    {
        [JsonProperty("result")]
        public ChatMember Result;
    }
}