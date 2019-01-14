using nerderies.TelegramBotApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using nerderies.TelegramBotApi.Interfaces;

namespace nerderies.TelegramBotApi
{
    public class TelegramBot
    {
        public enum MarkdownStyles
        {
            HTML,
            Markdown,
            None
        }
        #region public static operations

        //this is for easy use
        public static TelegramBot GetBot(string token, bool rateLimiting = false)
        {
            var c = new Communicator(token, rateLimiting ? Constants.DefaultRateLimitingTimeInMilliSeconds : 0);
            return new TelegramBot(c);
        }

        #endregion

        #region private members

        private object _syncObject = new object();
        private long _updateOffset = 0;
        private ICommunicator _communicator = null;

        #endregion

        #region public .ctor

        /// <summary>
        /// creates a new instance of a TelegramBot class
        /// </summary>
        /// <param name="authenticationToken">the authentication token you received from telegram.</param>
        public TelegramBot(ICommunicator communicator) => _communicator = communicator;

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
        /// Gets the User object of this bot.
        /// </summary>
        /// <returns>A User object with infos about the bot</returns>
        public User GetMe()
        {
            User me = _communicator.GetReply<GetMeReply>("getMe").Result;
            return me;
        }

        /// <summary>
        /// Sends a Message to a chat
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="text"></param>
        /// <returns>on success, the sent Message is returned </returns>
        public Message SendMessage(long chatId, string text, Message replyToMessage = null, MarkdownStyles markdownStyle = MarkdownStyles.None)
        {
            if (text.Length > Constants.MaxTextLength)
                text = text.Substring(0, Constants.MaxTextLength - 3) + "...";

            var parameters = new List<QueryStringParameter>
            {
                new QueryStringParameter("chat_id", chatId.ToString()),
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


            var result = _communicator.GetReply<SendMessageReply>("sendMessage", parameters.ToArray());
            if (result.OK)
                return result.sentMessage;
            else
                return null;
        }

        #endregion
    }
}
