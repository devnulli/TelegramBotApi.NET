using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetFileReply : Reply
    {
        [JsonProperty("result")]
        public FileDescriptor File;
    }
}
