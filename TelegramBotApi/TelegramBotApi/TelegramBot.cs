using nerderies.TelegramBotApi.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using TelegramBotApi.Interfaces;

namespace TelegramBotApi
{
    public class TelegramBot
    {
        #region public static operations

        //this is for easy use
        public static TelegramBot GetBot(string token)
        {
            var c = new Communicator(token);
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
        /// Gets all messages the bot has received since the last call. (max 100)
        /// </summary>
        /// <returns>a list of Messages</returns>
        /// 
        public IList<Message> GetUpdates()
        {
            QueryStringParameter requestlimit = new QueryStringParameter("limit", Constants.RequestLimit.ToString());

            UpdateReply reply;

            if (_updateOffset != 0)
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

        #endregion
    }
}
