using nerderies.TelegramBotApi.Converters;
using System.Text.Json.Serialization;
using System;

namespace nerderies.TelegramBotApi.DTOS
{
    public class Message
    {
        //complete API as of 2019-01-10

        [JsonPropertyName("message_id")]
        [JsonInclude]
        public long MessageId;

        [JsonPropertyName("from")]
        [JsonInclude]
        public User From;

        [JsonPropertyName("date")]
        [JsonInclude]
        public long DateInUnixTime;

        [JsonIgnore]
        public DateTime Date => UnixToDateTimeConverter.Convert(DateInUnixTime);

        [JsonPropertyName("chat")]
        [JsonInclude]
        public Chat Chat;

        [JsonPropertyName("forward_from")]
        [JsonInclude]
        public User ForwardFrom;

        [JsonPropertyName("forward_from_chat")]
        [JsonInclude]
        public Chat ForwardFromChat;

        [JsonPropertyName("forward_from_message_id")]
        [JsonInclude]
        public long ForwardFromMessageId;

        [JsonPropertyName("forward_signature")]
        [JsonInclude]
        public string ForwardSignature;

        [JsonPropertyName("forward_date")]
        [JsonInclude]
        public long ForwardDateInUnixTime;

        [JsonIgnore]
        public DateTime ForwardDate => UnixToDateTimeConverter.Convert(ForwardDateInUnixTime);

        [JsonPropertyName("reply_to_message")]
        [JsonInclude]
        public Message ReplyToMessage;

        [JsonPropertyName("edit_date")]
        [JsonInclude]
        public long EditDateInUnixTime;

        [JsonIgnore]
        public DateTime EditDate => UnixToDateTimeConverter.Convert(EditDateInUnixTime);

        [JsonPropertyName("media_group_id")]
        [JsonInclude]
        public string MediaGroupId;

        [JsonPropertyName("author_signature")]
        [JsonInclude]
        public string AuthorSignature;

        [JsonPropertyName("text")]
        [JsonInclude]
        public string Text;

        [JsonPropertyName("entities")]
        [JsonInclude]
        public MessageEntity[] Entities;

        [JsonPropertyName("caption_entities")]
        [JsonInclude]
        public MessageEntity[] CaptionEntities;

        [JsonPropertyName("audio")]
        [JsonInclude]
        public Audio Audio;

        [JsonPropertyName("document")]
        [JsonInclude]
        public Document Document;

        [JsonPropertyName("animation")]
        [JsonInclude]
        public Animation Animation;

        [JsonPropertyName("game")]
        [JsonInclude]
        public Game Game;

        [JsonPropertyName("photo")]
        [JsonInclude]
        public PhotoSize[] Photos;

        [JsonPropertyName("sticker")]
        [JsonInclude]
        public Sticker Sticker;

        [JsonPropertyName("video")]
        [JsonInclude]
        public Video Video;

        [JsonPropertyName("voice")]
        [JsonInclude]
        public Voice Voice;

        [JsonPropertyName("video_note")]
        [JsonInclude]
        public VideoNote VideoNote;

        [JsonPropertyName("caption")]
        [JsonInclude]
        public string Caption;

        [JsonPropertyName("contact")]
        [JsonInclude]
        public Contact Contact;

        [JsonPropertyName("location")]
        [JsonInclude]
        public Location Location;

        [JsonPropertyName("venue")]
        [JsonInclude]
        public Venue Venue;

        [JsonPropertyName("new_chat_members")]
        [JsonInclude]
        public User[] NewChatMembers;

        [JsonPropertyName("left_chat_members")]
        [JsonInclude]
        public User[] LeftChatMembers;

        [JsonPropertyName("new_chat_title")]
        [JsonInclude]
        public string NewChatTitle;

        [JsonPropertyName("new_chat_photo")]
        [JsonInclude]
        public PhotoSize[] NewChatPhotos;

        [JsonPropertyName("delete_chat_photo")]
        [JsonInclude]
        public bool DeleteChatPhoto;

        [JsonPropertyName("group_chat_created")]
        [JsonInclude]
        public bool GroupChatCreated;

        [JsonPropertyName("supergroup_chat_created")]
        [JsonInclude]
        public bool SuperGroupChatCreated;

        [JsonPropertyName("channel_chat_created")]
        [JsonInclude]
        public bool ChannelChatCreated;

        [JsonPropertyName("migrate_to_chat_id")]
        [JsonInclude]
        public long MigrateToChatId;

        [JsonPropertyName("migrate_from_chat_id")]
        [JsonInclude]
        public long MigrateFromChatId;

        [JsonPropertyName("pinned_message")]
        [JsonInclude]
        public Message PinnedMessage;

        [JsonPropertyName("invoice")]
        [JsonInclude]
        public Invoice Invoice;

        [JsonPropertyName("successful_payment")]
        [JsonInclude]
        public SuccessfulPayment SuccessfulPayment;

        [JsonPropertyName("connected_website")]
        [JsonInclude]
        public string ConnectedWebsite;

        [JsonPropertyName("passport_data")]
        [JsonInclude]
        public PassportData PassportData;
    }
}
