using nerderies.TelegramBotApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using nerderies.TelegramBotApi.Interfaces;

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
        /// <param name="chatId"></param>
        /// <param name="text"></param>
        /// <returns>on success, the sent Message is returned </returns>
        public Message SendMessage(Chat chat, string text, Message replyToMessage = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool disableWebPagePreview = false)
        {
            if (text.Length > Constants.MaxTextLength)
                text = text.Substring(0, Constants.MaxTextLength - 3) + "...";

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", chat.Id.ToString()),
                new QueryStringParameter("text", text)
            };

            if(replyToMessage!=null)
            {
                parameters.Add(new QueryStringParameter("reply_to_message_id", replyToMessage.MessageId.ToString()));
            }

            if(markdownStyle!= MarkdownStyles.None)
            {
                parameters.Add(new QueryStringParameter("parse_mode", Enum.GetName(typeof(MarkdownStyles), markdownStyle)));
            }

            if(disableWebPagePreview)
            {
                parameters.Add(new QueryStringParameter("disable_web_page_preview", true.ToString()));
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
        /// <param name="chat">the Chat to send the photo to</param>
        /// <param name="data">the content of the picture as byte array</param>
        /// <param name="fileName">the filename the picture has</param>
        /// <param name="mime"></param>
        /// <returns></returns>
        public Message SendPhoto(Chat chat, TelegramFile photo, string caption = null, MarkdownStyles markdownStyle = MarkdownStyles.None, bool disableNotification = false, Message replyToMessage = null)
        {
            var parameters = new List<MultiPartParameter>
            {
                new MultiPartStringParameter("chat_id", chat.Id.ToString()),
                photo.GetMultiPartParameter("photo")
            };

            if(caption!=null)
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
            var result = _communicator.GetMultiPartReply<SendPictureReply>("sendPhoto", parameters.ToArray());

            if (result.Ok)
                return result.SentMessage;
            else
                return null;
        }
        #endregion
    }
}
