using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendVideoNoteReply : Reply
    {
        [JsonProperty("result")]
        public Message SentMessage;
    }
}
