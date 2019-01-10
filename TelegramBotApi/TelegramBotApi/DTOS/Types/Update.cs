using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Update
    {
        //complete API as of 2019-01-10

        [JsonProperty("update_id")]
        public long UpdateId;

        [JsonProperty("message")]
        public Message Message;

        [JsonProperty("edited_message")]
        public Message EditedMessage;

        [JsonProperty("channel_post")]
        public Message ChannelPost;

        [JsonProperty("edited_channel_post")]
        public Message EditedChannelPost;

        [JsonProperty("inline_query")]
        public InlineQuery InlineQuery;

        [JsonProperty("chosen_inline_result")]
        public ChosenInlineResult ChosenInlineResult;

        [JsonProperty("callback_query")]
        public CallbackQuery CallbackQuery;

        [JsonProperty("shipping_query")]
        public ShippingQuery ShippingQuery;

        [JsonProperty("pre_checkout_query")]
        public PreCheckoutQuery PreCheckoutQuery;
    }
}
