﻿using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class SendVideoNoteReply : Reply
    {
        [JsonPropertyName("result")]
        [JsonInclude]
        public Message SentMessage;
    }
}
