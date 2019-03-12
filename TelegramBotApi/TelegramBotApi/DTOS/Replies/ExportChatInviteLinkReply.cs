using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ExportChatInviteLinkReply : Reply
    {
        [JsonProperty("result")]
        public string ChatInviteLink;
    }
}