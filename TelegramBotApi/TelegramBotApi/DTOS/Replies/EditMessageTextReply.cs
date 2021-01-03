using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class EditMessageTextReply : Reply
    {
        [JsonProperty("result")]
        public Message UpdatedMessage;
    }
}
