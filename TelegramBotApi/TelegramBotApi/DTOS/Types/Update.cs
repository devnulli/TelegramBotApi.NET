using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Update
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("update_id")]
        [JsonInclude]
        public long UpdateId;

        [JsonPropertyName("message")]
        [JsonInclude]
        public Message Message;

        [JsonPropertyName("edited_message")]
        [JsonInclude]
        public Message EditedMessage;

        [JsonPropertyName("channel_post")]
        [JsonInclude]
        public Message ChannelPost;

        [JsonPropertyName("edited_channel_post")]
        [JsonInclude]
        public Message EditedChannelPost;

        [JsonPropertyName("inline_query")]
        [JsonInclude]
        public InlineQuery InlineQuery;

        [JsonPropertyName("chosen_inline_result")]
        [JsonInclude]
        public ChosenInlineResult ChosenInlineResult;

        [JsonPropertyName("callback_query")]
        [JsonInclude]
        public CallbackQuery CallbackQuery;

        [JsonPropertyName("shipping_query")]
        [JsonInclude]
        public ShippingQuery ShippingQuery;

        [JsonPropertyName("pre_checkout_query")]
        [JsonInclude]
        public PreCheckoutQuery PreCheckoutQuery;
    }
}
