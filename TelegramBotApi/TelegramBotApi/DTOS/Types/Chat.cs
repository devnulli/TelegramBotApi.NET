using Newtonsoft.Json;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Chat
    {
        //complete API as of 2019-01-10

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("username")]
        public string UserName;

        [JsonProperty("first_name")]
        public string FirstName;

        [JsonProperty("last_name")]
        public string LastName;

        [JsonProperty("all_members_are_administrators")]
        public bool AllMembersAreAdministrators;

        [JsonProperty("photo")]
        public ChatPhoto Photo;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("invite_link")]
        public string InviteLink;

        [JsonProperty("pinned_message")]
        public Message PinnedMessage;

        [JsonProperty("sticker_set_name")]
        public string StickerSetName;

        [JsonProperty("can_set_sticker_set")]
        public string CanSetStickerSet;
    }
}
