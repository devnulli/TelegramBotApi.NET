using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class PhotoSize
    {
        //complete API as of 2019-01-11

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("width")]
        public string Width;

        [JsonProperty("height")]
        public long Height;

        [JsonProperty("file_size")]
        public long FileSize;
    }
}
