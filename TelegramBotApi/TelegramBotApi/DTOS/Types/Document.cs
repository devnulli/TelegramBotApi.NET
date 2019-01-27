using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Document
    {
        //complete API as of 2019-01-11

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("thumb")]
        public PhotoSize Thumb;

        [JsonProperty("file_name")]
        public string FileName;

        [JsonProperty("mime_type")]
        public string MimeType;

        [JsonProperty("file_size")]
        public long FileSize;
    }
}
