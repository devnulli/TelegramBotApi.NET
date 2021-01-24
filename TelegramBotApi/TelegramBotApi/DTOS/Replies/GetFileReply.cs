using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class GetFileReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public FileDescriptor File;
    }
}
