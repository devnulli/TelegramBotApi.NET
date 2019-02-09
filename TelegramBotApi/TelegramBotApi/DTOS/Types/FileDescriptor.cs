using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class FileDescriptor
    {
        //complete API as of 2019-02-09

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("file_size")]
        public long FileSize;

        [JsonProperty("file_path")]
        public string FilePath;
    }
}
