using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ChatPhoto
    {
        //complete API as of 2019-01-10

        [JsonProperty("small_file_id")]
        public string SmallFileId;

        [JsonProperty("big_file_id")]
        public string BigFileId;
    }
}
