using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.IntegrationTests
{
    public class TestObjects
    {
        [JsonPropertyName("testaudio")]
        [JsonInclude]
        public byte[] TestAudio;

        [JsonPropertyName("testthumb")]
        [JsonInclude]
        public byte[] TestThumb;

        [JsonPropertyName("testvideo")]
        [JsonInclude]
        public byte[] TestVideo;

        [JsonPropertyName("testphoto")]
        [JsonInclude]
        public byte[] TestPhoto;

        [JsonPropertyName("testanimation")]
        [JsonInclude]
        public byte[] TestAnimation;

        [JsonPropertyName("testvoice")]
        [JsonInclude]
        public byte[] TestVoice;
    }
}
