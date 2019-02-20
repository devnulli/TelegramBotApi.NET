using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ExportChatInviteLinkReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public string ChatInviteLink;
    }
}