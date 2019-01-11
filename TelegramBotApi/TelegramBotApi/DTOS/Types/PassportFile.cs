using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PassportFile
    {
        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("file_size")]
        public long FileSize;

        [JsonProperty("file_date")]
        public long FileDate;
    }
}