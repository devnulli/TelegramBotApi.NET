using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PassportFile
    {
        //complete API as of 2019-01-10

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("file_size")]
        public long FileSize;

        [JsonProperty("file_date")]
        public long FileDate;
    }
}