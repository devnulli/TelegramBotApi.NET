using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.IntegrationTests
{
    public class TestObjects
    {
        [JsonProperty("testaudio")]
        public byte[] TestAudio;

        [JsonProperty("testthumb")]
        public byte[] TestThumb;
    }
}
