using nerderies.TelegramBotApi.Converters;
using Newtonsoft.Json;
using System;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Message
    {
        //complete API as of 2019-01-10

        [JsonProperty("message_id")]
        public long MessageId;

        [JsonProperty("from")]
        public User From;

        [JsonProperty("date")]
        public long DateInUnixTime;

        [JsonIgnore]
        public DateTime Date => UnixToDateTimeConverter.Convert(DateInUnixTime);

        [JsonProperty("chat")]
        public Chat Chat;

        [JsonProperty("forward_from")]
        public User ForwardFrom;

        [JsonProperty("forward_from_chat")]
        public Chat ForwardFromChat;

        [JsonProperty("forward_from_message_id")]
        public long ForwardFromMessageId;

        [JsonProperty("forward_signature")]
        public string ForwardSignature;

        [JsonProperty("forward_date")]
        public long ForwardDateInUnixTime;

        [JsonIgnore]
        public DateTime ForwardDate => UnixToDateTimeConverter.Convert(ForwardDateInUnixTime);

        [JsonProperty("reply_to_message")]
        public Message ReplyToMessage;

        [JsonProperty("edit_date")]
        public long EditDateInUnixTime;

        [JsonIgnore]
        public DateTime EditDate => UnixToDateTimeConverter.Convert(EditDateInUnixTime);

        [JsonProperty("media_group_id")]
        public string MediaGroupId;

        [JsonProperty("author_signature")]
        public string AuthorSignature;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("entities")]
        public MessageEntity[] Entities;

        [JsonProperty("caption_entities")]
        public MessageEntity[] CaptionEntities;

        [JsonProperty("audio")]
        public Audio Audio;

        [JsonProperty("document")]
        public Document Document;

        [JsonProperty("animation")]
        public Animation Animation;

        [JsonProperty("game")]
        public Game Game;

        [JsonProperty("photo")]
        public PhotoSize[] Photos;

        [JsonProperty("sticker")]
        public Sticker Sticker;

        [JsonProperty("video")]
        public Video Video;

        [JsonProperty("voice")]
        public Voice Voice;

        [JsonProperty("video_note")]
        public VideoNote VideoNote;

        [JsonProperty("caption")]
        public string Caption;

        [JsonProperty("contact")]
        public Contact Contact;

        [JsonProperty("location")]
        public Location Location;

        [JsonProperty("venue")]
        public Venue Venue;

        [JsonProperty("new_chat_members")]
        public User[] NewChatMembers;

        [JsonProperty("left_chat_members")]
        public User[] LeftChatMembers;

        [JsonProperty("new_chat_title")]
        public string NewChatTitle;

        [JsonProperty("new_chat_photo")]
        public PhotoSize[] NewChatPhotos;

        [JsonProperty("delete_chat_photo")]
        public bool DeleteChatPhoto;

        [JsonProperty("group_chat_created")]
        public bool GroupChatCreated;

        [JsonProperty("supergroup_chat_created")]
        public bool SuperGroupChatCreated;

        [JsonProperty("channel_chat_created")]
        public bool ChannelChatCreated;

        [JsonProperty("migrate_to_chat_id")]
        public long MigrateToChatId;

        [JsonProperty("migrate_from_chat_id")]
        public long MigrateFromChatId;

        [JsonProperty("pinned_message")]
        public Message PinnedMessage;

        [JsonProperty("invoice")]
        public Invoice Invoice;

        [JsonProperty("successful_payment")]
        public SuccessfulPayment SuccessfulPayment;

        [JsonProperty("connected_website")]
        public string ConnectedWebsite;

        [JsonProperty("passport_data")]
        public PassportData PassportData;
    }
}
