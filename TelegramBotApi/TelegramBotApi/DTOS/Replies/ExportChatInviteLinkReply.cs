using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ExportChatInviteLinkReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public string ChatInviteLink;
    }
}