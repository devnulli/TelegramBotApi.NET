using Newtonsoft.Json;
using System.Xml.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class UpdateReply : Reply
    {
        //complete API as of 2019-01-10

        [JsonProperty("ok")]
        public bool Success;
        [JsonProperty("result")]
        public Update[] Updates;
    }
}
