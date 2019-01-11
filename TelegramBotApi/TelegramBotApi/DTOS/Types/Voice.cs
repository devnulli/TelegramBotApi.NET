using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Voice
    {
        //complete API as of 2019-01-11

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("duration")]
        public long Duration;

        [JsonProperty("mime_type")]
        public string MimeType;

        [JsonProperty("file_size")]
        public long FileSize;
    }
}
