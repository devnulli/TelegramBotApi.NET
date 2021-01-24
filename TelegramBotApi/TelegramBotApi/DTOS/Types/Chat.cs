using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Chat
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("id")]
        [JsonInclude]
        public long Id;

        [JsonPropertyName("type")]
        [JsonInclude]
        public string Type;

        [JsonPropertyName("title")]
        [JsonInclude]
        public string Title;

        [JsonPropertyName("username")]
        [JsonInclude]
        public string UserName;

        [JsonPropertyName("first_name")]
        [JsonInclude]
        public string FirstName;

        [JsonPropertyName("last_name")]
        [JsonInclude]
        public string LastName;

        [JsonPropertyName("all_members_are_administrators")]
        [JsonInclude]
        public bool AllMembersAreAdministrators;

        [JsonPropertyName("photo")]
        [JsonInclude]
        public ChatPhoto Photo;

        [JsonPropertyName("description")]
        [JsonInclude]
        public string Description;

        [JsonPropertyName("invite_link")]
        [JsonInclude]
        public string InviteLink;

        [JsonPropertyName("pinned_message")]
        [JsonInclude]
        public Message PinnedMessage;

        [JsonPropertyName("sticker_set_name")]
        [JsonInclude]
        public string StickerSetName;

        [JsonPropertyName("can_set_sticker_set")]
        [JsonInclude]
        public string CanSetStickerSet;
    }
}
