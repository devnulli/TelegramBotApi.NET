using nerderies.TelegramBotApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using nerderies.TelegramBotApi.Interfaces;
using System.Globalization;

namespace nerderies.TelegramBotApi
{
    public class TelegramBot
    {
        #region public types

        public enum MarkdownStyles
        {
            HTML,
            Markdown,
            None
        }

        #endregion

        #region public static operations

        public static TelegramBot GetBot(string token, bool rateLimiting = false)
        {
            var c = new Communicator(token, rateLimiting ? Constants.DefaultRateLimitingTimeInMilliSeconds : 0);
            return new TelegramBot(c);
        }

        #endregion

        #region private members

        private readonly object _syncObject = new object();
        private long _updateOffset = 0;
        private ICommunicator _communicator = null;

        #endregion

        #region public .ctor

        /// <summary>
        /// creates a new instance of a TelegramBot class
        /// </summary>
        /// <param name="authenticationToken">the authentication token you received from telegram.</param>
        public TelegramBot(ICommunicator communicator)
        {
            _communicator = communicator ?? throw new ArgumentNullException();
        }

        #endregion

        #region public operations

        /// <summary>
        /// Gets all unconfirmed updates the bot has received
        /// - when called the first time, it gets all unconfirmed messages the bot has received
        /// - subsequent calls confirm previous update, so after that, it will only get new updates (and confirm old ones) 
        /// 
        /// </summary>
        /// <param name="peek">when set, the bot will not confirm updates, so that they will also be kept on the server</param>
        /// <returns>a list of Messages</returns>
        /// 
        public IList<Update> GetUpdates(bool peek = false)
        {
            QueryStringParameter requestlimit = new QueryStringParameter("limit", Constants.RequestLimit.ToString());

            UpdateReply reply;

            if (_updateOffset != 0 && !peek)
            {
                QueryStringParameter offset = new QueryStringParameter("offset", _updateOffset.ToString());
                reply = _communicator.GetReply<UpdateReply>("getUpdates", requestlimit, offset);
            }
            else
            {
               reply = _communicator.GetReply<UpdateReply>("getUpdates", requestlimit);
            }

            //set offset for next pull
           if (reply.Updates.Length > 0)
                _updateOffset = reply.Updates[reply.Updates.Length - 1].UpdateId + 1;

            return new List<Update>(reply.Updates);
        }

        /// <summary>
        /// Gets the User object for this bot.
        /// </summary>
        /// <returns>A User object with infos about the bot</returns>
        public User GetMe()
        {
            User me = _communicator.GetReply<GetMeReply>("getMe").Result;
            return me;
        }

        /// <summary>
        /// Sends a message to a chat
        /// </summary>
        /// <returns>on success, the sent Message is returned </returns>
        public Message SendMessage(Chat chat, string text, MarkdownStyles markdownStyle = MarkdownStyles.None, bool disableWebPagePreview = false, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || text == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("text cannot be empty");

            if (text.Length > Constants.MaxTextLength)
                text = text.Substring(0, Constants.MaxTextLength - 3) + "...";

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("text", text)
            };

            if(markdownStyle!= MarkdownStyles.None)
            {
                parameters.Add(new QueryStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));
            }

            if(disableWebPagePreview)
            {
                parameters.Add(new QueryStringParameter("disable_web_page_preview", true.ToString()));
            }

            if (disableNotification)
            {
                parameters.Add(new QueryStringParameter("disable_notification", true.ToString()));
            }

            if (replyToMessage != null)
            {
                parameters.Add(new QueryStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));
            }

            var result = _communicator.GetReply<SendMessageReply>("sendMessage", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// forwards a message 
        /// </summary>
        /// <param name="messageToForward">the Message that will be forwarded</param>
        /// <param name="chatToForwardTo">the Chat to forward it to</param>
        /// <param name="disableNotification">forwards it without notification when set to true</param>
        /// <returns>if successful, the sent message is returned</returns>
        public Message ForwardMessage(Message messageToForward, Chat chatToForwardTo, bool disableNotification = false)
        {
            if (chatToForwardTo == null || messageToForward == null)
                throw new ArgumentNullException();

            List<QueryStringParameter> parameters = new List<QueryStringParameter>()
            {
                new QueryStringParameter("chat_id", chatToForwardTo.Id.ToString()),
                new QueryStringParameter("from_chat_id", messageToForward.Chat.Id.ToString()),
                new QueryStringParameter("message_id", messageToForward.MessageId.ToString())
            };

            var result = _communicator.GetReply<ForwardMessageReply>("forwardMessage", parameters.ToArray());
            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends a photo to the chat
        /// </summary>
        /// <returns>on success, the sent Message is returned </returns>
        public Message SendPhoto(Chat chat, InputFile photo, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || photo == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                photo.GetMultiPartParameter("photo")
            };

            if(!string.IsNullOrEmpty(caption))
            {
                parameters.Add(new MultiPartStringParameter("caption", caption));
            }

            if (markdownStyle != MarkdownStyles.None)
            {
                parameters.Add(new MultiPartStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));
            }

            if (disableNotification)
            {
                parameters.Add(new MultiPartStringParameter("disable_notification", disableNotification.ToString()));
            }

            if (replyToMessage != null)
            {
                parameters.Add(new MultiPartStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));
            }

            var result = _communicator.GetMultiPartReply<SendPhotoReply>("sendPhoto", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends an audio file to the chat
        /// </summary>
        /// <returns>on success, the sent Message is returned</returns>
        public Message SendAudio(Chat chat, InputFile audio, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, long duration = long.MinValue, string performer = null, string title = null, InputFile thumb = null, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || audio == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                audio.GetMultiPartParameter("audio")
            };

            if (!string.IsNullOrEmpty(caption))
                parameters.Add(new MultiPartStringParameter("caption", caption));

            if (markdownStyle != MarkdownStyles.None)
                parameters.Add(new MultiPartStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));

            if(duration > long.MinValue)
                parameters.Add(new MultiPartStringParameter("duration", duration.ToString()));

            if (!string.IsNullOrEmpty(performer))
                parameters.Add(new MultiPartStringParameter("performer", performer));

            if (!string.IsNullOrEmpty(title))
                parameters.Add(new MultiPartStringParameter("title", title));

            if (thumb != null)
                parameters.Add(thumb.GetMultiPartParameter("thumb"));

            if (disableNotification)
                parameters.Add(new MultiPartStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new MultiPartStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetMultiPartReply<SendAudioReply>("sendAudio", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends a document to the chat
        /// </summary>
        /// <returns>on success, the sent message is returned</returns>
        public Message SendDocument(Chat chat, InputFile document, InputFile thumb = null, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool disableNotification = false, Message replyToMessage = null )
        {
            if (chat == null || document == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>()
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                document.GetMultiPartParameter("document")
            };

            if (thumb != null)
                parameters.Add(thumb.GetMultiPartParameter("thumb"));

            if (!string.IsNullOrEmpty(caption))
                parameters.Add(new MultiPartStringParameter("caption", caption));

            if (markdownStyle != MarkdownStyles.None)
                parameters.Add(new MultiPartStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));

            if (disableNotification)
                parameters.Add(new MultiPartStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new MultiPartStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetMultiPartReply<SendDocumentReply>("sendDocument", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends a video to the chat
        /// </summary>
        /// <returns>on success, the sent message is returned</returns>
        public Message SendVideo(Chat chat, InputFile video, long duration = long.MinValue, long width = long.MinValue, long height = long.MinValue, InputFile thumb = null, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool supportsStreaming = false, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || video == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>()
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                video.GetMultiPartParameter("video")
            };

            if (duration > long.MinValue)
                parameters.Add(new MultiPartStringParameter("duration", duration.ToString()));

            if (width > long.MinValue)
                parameters.Add(new MultiPartStringParameter("width", duration.ToString()));

            if (height > long.MinValue)
                parameters.Add(new MultiPartStringParameter("height", duration.ToString()));

            if (thumb != null)
                parameters.Add(thumb.GetMultiPartParameter("thumb"));

            if (!string.IsNullOrEmpty(caption))
                parameters.Add(new MultiPartStringParameter("caption", caption));

            if (markdownStyle != MarkdownStyles.None)
                parameters.Add(new MultiPartStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));

            if (supportsStreaming)
                parameters.Add(new MultiPartStringParameter("supports_streaming", true.ToString()));

            if (disableNotification)
                parameters.Add(new MultiPartStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new MultiPartStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetMultiPartReply<SendVideoReply>("sendVideo", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends a video to the chat
        /// </summary>
        /// <returns>on success, the sent message is returned</returns>
        public Message SendAnimation(Chat chat, InputFile animation, long duration = long.MinValue, long width = long.MinValue, long height = long.MinValue, InputFile thumb = null, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool supportsStreaming = false, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || animation == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>()
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                animation.GetMultiPartParameter("animation")
            };

            if (duration > long.MinValue)
                parameters.Add(new MultiPartStringParameter("duration", duration.ToString()));

            if (width > long.MinValue)
                parameters.Add(new MultiPartStringParameter("width", duration.ToString()));

            if (height > long.MinValue)
                parameters.Add(new MultiPartStringParameter("height", duration.ToString()));

            if (thumb != null)
                parameters.Add(thumb.GetMultiPartParameter("thumb"));

            if (!string.IsNullOrEmpty(caption))
                parameters.Add(new MultiPartStringParameter("caption", caption));

            if (markdownStyle != MarkdownStyles.None)
                parameters.Add(new MultiPartStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));

            if (disableNotification)
                parameters.Add(new MultiPartStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new MultiPartStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetMultiPartReply<SendAnimationReply>("sendAnimation", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends a voice message to the chat
        /// </summary>
        /// <returns>on success, the sent mesage is returned</returns>
        public Message SendVoice(Chat chat, InputFile voice, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, long duration = long.MinValue, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || voice == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>()
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                voice.GetMultiPartParameter("voice")
            };

            if (!string.IsNullOrEmpty(caption))
                parameters.Add(new MultiPartStringParameter("caption", caption));

            if (markdownStyle != MarkdownStyles.None)
                parameters.Add(new MultiPartStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));

            if (duration > long.MinValue)
                parameters.Add(new MultiPartStringParameter("duration", duration.ToString()));

            if (disableNotification)
                parameters.Add(new MultiPartStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new MultiPartStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetMultiPartReply<SendVoiceReply>("sendVoice", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends a video note to the chat
        /// </summary>
        /// <returns>on success, the sent message is returned</returns>
        public Message SendVideoNote(Chat chat, InputFile videoNote, long duration = long.MinValue, long length = long.MinValue, InputFile thumb = null, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || videoNote == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>()
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                videoNote.GetMultiPartParameter("video_note")
            };

            if (duration > long.MinValue)
                parameters.Add(new MultiPartStringParameter("duration", duration.ToString()));

            if (length > long.MinValue)
                parameters.Add(new MultiPartStringParameter("length", length.ToString()));

            if (thumb != null)
                parameters.Add(thumb.GetMultiPartParameter("thumb"));

            if (disableNotification)
                parameters.Add(new MultiPartStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new MultiPartStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetMultiPartReply<SendVideoNoteReply>("sendVideoNote", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// sends a location to the chat
        /// </summary>
        /// <returns>on success, the sent location is returned</returns>
        public Message SendLocation(Chat chat, Location location, long livePeriod = long.MinValue, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || location == null)
                throw new ArgumentNullException();

            if (livePeriod != long.MinValue && (livePeriod < 60 || livePeriod > 86400))
                throw new ArgumentException("Liveperiod should be between 60 and 86400");

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("latitude", location.Latitude.ToString(CultureInfo.InvariantCulture)),
                new QueryStringParameter("longitude", location.Longitude.ToString(CultureInfo.InvariantCulture))
            };

            if (livePeriod != long.MinValue)
                parameters.Add(new QueryStringParameter("live_period", livePeriod.ToString()));

            if (disableNotification)
                parameters.Add(new QueryStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new QueryStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetReply<SendLocationReply>("sendLocation", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// edit live location messages sent by the bot or via the bot (for inline bots). A location can be edited until its live_period expires or editing is explicitly disabled by a call to stopMessageLiveLocation
        /// </summary>
        /// <returns>true if successful, also sets the sentMessage out parameter when the edited message was sent by the bot</returns>
        /// 
        //the api has different return values for inline messages, so when needed, make a different method i.e. EditInlineMessageLiveLocation
        public Message EditMessageLiveLocation(Message messageToUpdate, Location newLocation)
        {
            if (messageToUpdate == null || newLocation == null)
                throw new ArgumentNullException();

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", messageToUpdate.Chat.Id.ToString()),
                new QueryStringParameter("message_id", messageToUpdate.MessageId.ToString()),
                new QueryStringParameter("latitude", newLocation.Latitude.ToString(CultureInfo.InvariantCulture)),
                new QueryStringParameter("longitude", newLocation.Longitude.ToString(CultureInfo.InvariantCulture))
            };

            var result = _communicator.GetReply<EditMessageLiveLocationReply>("editMessageLiveLocation", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// Use this method to stop updating a live location message sent by the bot or via the bot (for inline bots) before live_period expires
        /// </summary>
        /// <returns></returns>
        //the api has different return values for inline messages, so when needed, make a different method i.e. StopInlineMessageLiveLocation
        public Message StopMessageLiveLocation(Message messageToStop)
        {
            if (messageToStop == null)
                throw new ArgumentNullException();

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", messageToStop.Chat.Id.ToString()),
                new QueryStringParameter("message_id", messageToStop.MessageId.ToString()),
            };

            var result = _communicator.GetReply<StopMessageLiveLocationReply>("stopMessageLiveLocation", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// Use this method to send information about a venue.
        /// </summary>
        /// <returns>on success, the sent Message is returned</returns>
        public Message SendVenue(Chat chat, Location location, string title = null, string address = null, string foursquareId = null, string foursquareType = null, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || location == null)
                throw new ArgumentNullException();

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("latitude", location.Latitude.ToString(CultureInfo.InvariantCulture)),
                new QueryStringParameter("longitude", location.Longitude.ToString(CultureInfo.InvariantCulture))
            };

            if(!string.IsNullOrEmpty(title))
                parameters.Add(new QueryStringParameter("title", title));

            if (!string.IsNullOrEmpty(address))
                parameters.Add(new QueryStringParameter("address", address));

            if (!string.IsNullOrEmpty(foursquareId))
                parameters.Add(new QueryStringParameter("foursquare_id", foursquareId));

            if (!string.IsNullOrEmpty(foursquareType))
                parameters.Add(new QueryStringParameter("foursquare_type", foursquareType));

            if (disableNotification)
                parameters.Add(new QueryStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new QueryStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetReply<SendVenueReply>("sendVenue", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// Use this method to send phone contacts
        /// </summary>
        /// <returns>On success, the sent Message is returned.</returns>
        public Message SendContact(Chat chat, string phoneNumber, string firstName, string lastName = null, string vcard = null, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || phoneNumber == null || firstName == null)
                throw new ArgumentNullException();

            if (string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(firstName))
                throw new ArgumentException();

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("phone_number", phoneNumber),
                new QueryStringParameter("first_name", firstName)
            };

            if (!string.IsNullOrEmpty(lastName))
                parameters.Add(new QueryStringParameter("last_name", lastName));

            if (!string.IsNullOrEmpty(vcard))
                parameters.Add(new QueryStringParameter("vcard", vcard));

            if (disableNotification)
                parameters.Add(new QueryStringParameter("disable_notification", disableNotification.ToString()));

            if (replyToMessage != null)
                parameters.Add(new QueryStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));

            var result = _communicator.GetReply<SendContactReply>("sendContact", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

        /// <summary>
        /// The status is set for 5 seconds or less (when a message arrives from your bot, Telegram clients also clear your bot status)
        /// </summary>
        /// <returns>True if successful</returns>
        public bool SendChatAction(Chat chat, ChatAction chatAction)
        {
            var parameters = new List<QueryStringParameter>()
            {
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("action", chatAction.Code)
            };

            var reply = _communicator.GetReply<SendChatActionReply>("sendChatAction", parameters.ToArray());

            if (reply.Ok)
            {
                return reply.Result;
            }
            else
                return false;

        }

        /// <summary>
        /// returns the profile photos of a user in different qualities
        /// </summary>
        /// <returns>A list of Photosize arrays, where each list element contains different qualities of the photo in an array</returns>
        public IList<PhotoSize[]> GetUserProfilePhotos(User user, int maxPhotos = int.MaxValue)
        {
            if (user == null)
                throw new ArgumentNullException();

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("user_id", user.Id.ToString()),
            };

            long offset = 0;
            long stepsize = 100;
            var offsetParameter = new QueryStringParameter("offset", offset.ToString());
            var stepSizeParameter = new QueryStringParameter("limit", maxPhotos < stepsize ? maxPhotos.ToString() : stepsize.ToString());
            

            parameters.Add(offsetParameter);
            parameters.Add(stepSizeParameter);

            long totalCount;
            var photos = new List<PhotoSize[]>();

            do
            {
                var reply = _communicator.GetReply<GetUserProfilePhotosReply>("getUserProfilePhotos", parameters.ToArray());
                if (!reply.Ok)
                    return null;

                totalCount = reply.UserProfilePhotos.TotalCount;
                photos.AddRange(reply.UserProfilePhotos.Photos);

                offset += stepsize;
                offsetParameter.Value = offset.ToString();
                if (totalCount - offset < stepsize)
                    stepSizeParameter.Value = (totalCount - offset).ToString();
            }
            while ((offset <= totalCount) && (offset <= maxPhotos));

            return photos;
        }

        /// <summary>
        /// returns a FileDescriptor object which contains info about a file and can be used to download the file
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public FileDescriptor GetFile(string fileId)
        {
            if (fileId == null)
                throw new ArgumentNullException();

            var id = new QueryStringParameter("file_id", fileId);
            var reply = _communicator.GetReply<GetFileReply>("getFile", id);

            if (reply.Ok)
                return reply.File;
            else
                return null;
        }

        /// <summary>
        /// Tries to download the content of a File described in a FileDescriptor object
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public byte[] GetFileContent(FileDescriptor result)
        {
            return _communicator.GetFileContent(result.FilePath);
        }

        /// <summary>
        /// generates a new chat invitation link, renders any previously generated invitation links useless.
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        public string ExportChatInviteLink(Chat chat)
        {
            if (chat == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetReply<ExportChatInviteLinkReply>("exportChatInviteLink", new QueryStringParameter("chat_id", chat.Id.ToString()));
            if (reply.Ok)
                return reply.ChatInviteLink;
            else
                return null;
        }

        /// <summary>
        /// changes or sets the chat photo of the given chat. bot must be channel admin or in a "all admin" chat
        /// </summary>
        /// <returns>true on success</returns>
        public bool SetChatPhoto(Chat chat, InputFile photo)
        {
            if (chat == null || photo == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetMultiPartReply<SetChatPhotoReply>("setChatPhoto",
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                photo.GetMultiPartParameter("photo"));

            return reply.Ok && reply.Success;
        }

        /// <summary>
        /// deletes the chat photo from the given chat. bot must be channel admin or in a "all admin" chat
        /// </summary>
        /// <returns>true on success</returns>
        public bool DeleteChatPhoto(Chat chat)
        {
            if (chat == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetMultiPartReply<DeleteChatPhotoReply>("deleteChatPhoto",
                new MultiPartStringParameter("chat_id", chat.Id.ToString()));

            return reply.Ok && reply.Success;
        }

        /// <summary>
        /// sets the title of a chat. 
        /// </summary>
        /// <returns>true on success</returns>
        public bool SetChatTitle(Chat chat, string title)
        {
            if (chat == null || title == null)
                throw new ArgumentNullException();

            if (title.Length > 255  || title.Length < 1)
                throw new ArgumentException("title must be between 1 and 255 characters");

            var reply = _communicator.GetReply<SetChatTitleReply>("setChatTitle",
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("title", title));

            return reply.Ok && reply.Success;
        }

        /// <summary>
        /// sets the description of a chat. 
        /// </summary>
        /// <returns>true on success</returns>
        public bool SetChatDescription(Chat chat, string description)
        {
            if (chat == null || description == null)
                throw new ArgumentNullException();

            if (description.Length > 255)
                throw new ArgumentException("title must be between 0 and 255 characters");

            var reply = _communicator.GetReply<SetChatDescriptionReply>("setChatDescription",
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("description", description));

            return reply.Ok && reply.Success;
        }

        /// <summary>
        /// pins a message to the chat 
        /// </summary>
        /// <returns>true on success</returns>
        public bool PinChatMessage(Message message)
        {
            if (message == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetReply<PinChatMessageReply>("pinChatMessage",
                new QueryStringParameter("chat_id", message.Chat.Id.ToString()),
                new QueryStringParameter("message_id", message.MessageId.ToString()));

            return reply.Ok && reply.Success;
        }

        /// <summary>
        /// unpins messages from the chat
        /// </summary>
        /// <returns>true on success</returns>
        public bool UnpinChatMessage(Chat chat)
        {
            if (chat == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetReply<UnpinChatMessageReply>("unpinChatMessage",
                new QueryStringParameter("chat_id", chat.Id.ToString()));

            return reply.Ok && reply.Success;
        }

        /// <summary>
        /// Leaves a Chat 
        /// </summary>
        /// <returns>true on success</returns>
        //this is not integration tested due to the lack of a JOIN CHAT method
        public bool LeaveChat(Chat chat)
        {
            if (chat == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetReply<LeaveChatReply>("leaveChat",
                new QueryStringParameter("chat_id", chat.Id.ToString()));

            return reply.Ok && reply.Success;
        }

        /// <summary>
        /// Gets info about a chat.
        /// </summary>
        /// <returns>a Chat object or null if not successful</returns>
        public Chat GetChat(long chatId)
        {
            var reply = _communicator.GetReply<GetChatReply>("getChat",
                new QueryStringParameter("chat_id", chatId.ToString()));

            if (reply.Ok)
                return reply.Chat;
            else
                return null;
        }

        /// <summary>
        /// Gets the administrators of a chat.
        /// </summary>
        /// <returns>returns an Array of ChatMembers that contains information about all chat administrators except other bots</returns>
        public IList<ChatMember> GetChatAdministrators(Chat chat)
        {
            if (chat == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetReply<GetChatAdministratorsReply>("getChatAdministrators",
                new QueryStringParameter("chat_id", chat.Id.ToString()));

            if (reply.Ok)
                return new List<ChatMember>(reply.Administrators);
            else
                return null;
        }

        /// <summary>
        /// Use this method to get the number of members in a chat.
        /// </summary>
        /// <returns>the number of members in a chat</returns>
        public long GetChatMembersCount(Chat chat)
        {
            if (chat == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetReply<GetChatMembersCountReply>("getChatMembersCount",
                new QueryStringParameter("chat_id", chat.Id.ToString()));

            if (reply.Ok)
                return reply.ChatMembersCount;
            else
                throw new Exception("Could not get ChatMembers count for this chat");
            
        }

        /// <summary>
        /// Use this method to get information about a member of a chat.
        /// </summary>
        /// <returns>Returns a ChatMember object on success.</returns>
        public ChatMember GetChatMember(Chat chat, User user)
        {
            if (user == null)
                throw new ArgumentNullException();

            return GetChatMember(chat, user.Id);
        }

        /// <summary>
        /// Use this method to get information about a member of a chat. 
        /// this seems to not work for channelposts, because they do not appear to return the sending user as of 2019-02-25
        /// </summary>
        /// <returns>Returns a ChatMember object on success.</returns>
        public ChatMember GetChatMember(Chat chat, long userId)
        {
            if (chat == null)
                throw new ArgumentNullException();

            var reply = _communicator.GetReply<GetChatMemberReply>("getChatMember",
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("user_id", userId.ToString()));

            if (reply.Ok)
                return reply.Result;
            else
                return null;
        }
        #endregion
    }
}
