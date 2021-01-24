using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Sticker
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("file_id")]
        [JsonInclude]
        public string FileId;

        [JsonPropertyName("width")]
        [JsonInclude]
        public long Width;

        [JsonPropertyName("height")]
        [JsonInclude]
        public long Height;

        [JsonPropertyName("thumb")]
        [JsonInclude]
        public PhotoSize Thumb;

        [JsonPropertyName("emoji")]
        [JsonInclude]
        public string Emoji;

        [JsonPropertyName("set_name")]
        [JsonInclude]
        public string SetName;

        [JsonPropertyName("mask_position")]
        [JsonInclude]
        public MaskPosition MaskPosition;

        [JsonPropertyName("file_size")]
        [JsonInclude]
        public long FileSize;
    }
}
