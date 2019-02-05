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
        /// Gets all unconfirmed messages the bot has received
        /// - when called the first time, it get all unconfirmed messages the bot has received
        /// - subsequent calls confirm previous messages, so after that, it will only get new messages (and confirm old ones) 
        /// 
        /// </summary>
        /// <param name="peek">when set, the bot will not confirm updates, so that the messages will also be kept on the server</param>
        /// <returns>a list of Messages</returns>
        /// 
        public IList<Message> GetUpdates(bool peek = false)
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

            return new List<Message>(from u in reply.Updates select u.Message);
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
        public Message SendPhoto(Chat chat, TelegramFile photo, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool disableNotification = false, Message replyToMessage = null)
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
        public Message SendAudio(Chat chat, TelegramFile audio, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, long duration = long.MinValue, string performer = null, string title = null, TelegramFile thumb = null, bool disableNotification = false, Message replyToMessage = null)
        {
            if (chat == null || audio == null)
                throw new ArgumentNullException();

            var parameters = new List<MultiPartParameter>
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                audio.GetMultiPartParameter("audio")
            };

            if (caption != null)
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
        public Message SendDocument(Chat chat, TelegramFile document, TelegramFile thumb = null, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool disableNotification = false, Message replyToMessage = null )
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
        public Message SendVideo(Chat chat, TelegramFile video, long duration = long.MinValue, long width = long.MinValue, long height = long.MinValue, TelegramFile thumb = null, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool supportsStreaming = false, bool disableNotification = false, Message replyToMessage = null)
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
        public Message SendAnimation(Chat chat, TelegramFile animation, long duration = long.MinValue, long width = long.MinValue, long height = long.MinValue, TelegramFile thumb = null, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool supportsStreaming = false, bool disableNotification = false, Message replyToMessage = null)
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

            if (caption != null)
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
        public Message SendVoice(Chat chat, TelegramFile voice, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, long duration = long.MinValue, bool disableNotification = false, Message replyToMessage = null)
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
        public Message SendVideoNote(Chat chat, TelegramFile videoNote, long duration = long.MinValue, long length = long.MinValue, TelegramFile thumb = null, bool disableNotification = false, Message replyToMessage = null)
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

        public Message StopMessageLiveLocation(Message messageToUpdate)
        {
            if (messageToUpdate == null)
                throw new ArgumentNullException();

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", messageToUpdate.Chat.Id.ToString()),
                new QueryStringParameter("message_id", messageToUpdate.MessageId.ToString()),
            };

            var result = _communicator.GetReply<StopMessageLiveLocationReply>("stopMessageLiveLocation", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }

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
        #endregion
    }
}
