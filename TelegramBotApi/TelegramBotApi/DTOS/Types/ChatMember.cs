using nerderies.TelegramBotApi.Converters;
using Newtonsoft.Json;
using System;

namespace nerderies.TelegramBotApi.DTOS
{
    public class ChatMember
    {
        //complete API as of 2019-02-23

        [JsonProperty("user")]
        public User user;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("until_date")]
        public long UntilDateInUnixTime;

        [JsonIgnore]
        public DateTime Date => UnixToDateTimeConverter.Convert(UntilDateInUnixTime);

        [JsonProperty("can_be_edited")]
        public bool? CanBeEdited;

        [JsonProperty("can_change_info")]
        public bool? CanChangeInfo;

        [JsonProperty("can_post_messages")]
        public bool? CanPostMessages;

        [JsonProperty("can_edit_messages")]
        public bool? CanEditMessages;

        [JsonProperty("can_delete_messages")]
        public bool? CanDeleteMessages;

        [JsonProperty("can_invite_users")]
        public bool? CanInviteUsers;

        [JsonProperty("can_restrict_members")]
        public bool? CanRestrictMembers;

        [JsonProperty("can_pin_messages")]
        public bool? CanPinMessages;

        [JsonProperty("can_promote_members")]
        public bool? CanPromoteMembers;

        [JsonProperty("can_send_messages")]
        public bool? CanSendMessages;

        [JsonProperty("can_send_media_messages")]
        public bool? CanSendMediaMessages;

        [JsonProperty("can_send_other_messages")]
        public bool? CanSendOtherMessages;

        [JsonProperty("can_add_web_page_previews")]
        public bool? CanAddWebPagePreviews;
    }
}
