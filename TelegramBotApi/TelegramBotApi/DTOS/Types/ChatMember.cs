using nerderies.TelegramBotApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ChatMember
    {
        //complete API as of 2019-02-23

        [JsonPropertyName("user")]
        [JsonInclude]
        public User user;

        [JsonPropertyName("status")]
        [JsonInclude]
        public string Status;

        [JsonPropertyName("until_date")]
        [JsonInclude]
        public long UntilDateInUnixTime;

        [JsonIgnore]
        public DateTime Date => UnixToDateTimeConverter.Convert(UntilDateInUnixTime);

        [JsonPropertyName("can_be_edited")]
        [JsonInclude]
        public bool? CanBeEdited;

        [JsonPropertyName("can_change_info")]
        [JsonInclude]
        public bool? CanChangeInfo;

        [JsonPropertyName("can_post_messages")]
        [JsonInclude]
        public bool? CanPostMessages;

        [JsonPropertyName("can_edit_messages")]
        [JsonInclude]
        public bool? CanEditMessages;

        [JsonPropertyName("can_delete_messages")]
        [JsonInclude]
        public bool? CanDeleteMessages;

        [JsonPropertyName("can_invite_users")]
        [JsonInclude]
        public bool? CanInviteUsers;

        [JsonPropertyName("can_restrict_members")]
        [JsonInclude]
        public bool? CanRestrictMembers;

        [JsonPropertyName("can_pin_messages")]
        [JsonInclude]
        public bool? CanPinMessages;

        [JsonPropertyName("can_promote_members")]
        [JsonInclude]
        public bool? CanPromoteMembers;

        [JsonPropertyName("can_send_messages")]
        [JsonInclude]
        public bool? CanSendMessages;

        [JsonPropertyName("can_send_media_messages")]
        [JsonInclude]
        public bool? CanSendMediaMessages;

        [JsonPropertyName("can_send_other_messages")]
        [JsonInclude]
        public bool? CanSendOtherMessages;

        [JsonPropertyName("can_add_web_page_previews")]
        [JsonInclude]
        public bool? CanAddWebPagePreviews;
    }
}
