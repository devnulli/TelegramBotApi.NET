using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Game
    {
        //complete API as of 2019-01-11

        [JsonPropertyName("title")]
        [JsonInclude]
        public string Title;

        [JsonPropertyName("description")]
        [JsonInclude]
        public string Description;

        [JsonPropertyName("photo")]
        [JsonInclude]
        public PhotoSize[] Photos;

        [JsonPropertyName("text")]
        [JsonInclude]
        public string Text;

        [JsonPropertyName("text_entities")]
        [JsonInclude]
        public MessageEntity[] text_entities;

        [JsonPropertyName("animation")]
        [JsonInclude]
        public Animation Animation;
}
}
