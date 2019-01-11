using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Sticker
    {
        //complete API as of 2019-01-11

        [JsonProperty("file_id")]
        public string FileId;

        [JsonProperty("width")]
        public long Width;

        [JsonProperty("height")]
        public long Height;

        [JsonProperty("thumb")]
        public PhotoSize Thumb;

        [JsonProperty("emoji")]
        public string Emoji;

        [JsonProperty("set_name")]
        public string SetName;

        [JsonProperty("mask_position")]
        public MaskPosition MaskPosition;

        [JsonProperty("file_size")]
        public long FileSize;
    }
}
