using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Game
    {
        //complete API as of 2019-01-11

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("photo")]
        public PhotoSize[] Photos;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("text_entities")]
        public MessageEntity[] text_entities;

        [JsonProperty("animation")]
        public Animation Animation;
}
}
