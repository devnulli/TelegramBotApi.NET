using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetFileReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public FileDescriptor File;
    }
}
