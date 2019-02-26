﻿using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SetChatStickerSetReply : Reply
    {
        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("result")]
        public bool Success;
    }
}