using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class FileDescriptor
    {
        //complete API as of 2019-02-09

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;

        [JsonPropertyName("file_path")]
        [JsonInclude]
        public string FilePath;
    }
}
