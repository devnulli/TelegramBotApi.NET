using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using nerderies.TelegramBotApi.DTOS;
using Newtonsoft.Json;
using TelegramBotApi.Interfaces;

namespace TelegramBotApi
{
    internal class Communicator : ICommunicator
    {
        #region private members

        private string _authenticationToken = null;
        private WebClient _webClient = new WebClient();

        #endregion

        #region .ctor

        public Communicator(string authenticationToken)
        {
            _authenticationToken = authenticationToken;
        }

        #endregion

        #region private operations

        private string BuildOperationUrl(string methodName, QueryStringParameter[] parameters)
        {
            string url = Constants.ApiUrl.Replace("%%token%%", _authenticationToken) + "/" + methodName;

            if (parameters != null && parameters.Length > 0)
            {
                url = url + "?";
                bool first = true;
                foreach (var param in parameters)
                {
                    if (!first)
                    {
                        url = url + "&";
                    }
                    else
                        first = false;

                    url = url + Uri.EscapeDataString(param.Field) + "=" + Uri.EscapeDataString(param.Value);
                }
            }

            return url;
        }

        #endregion

        #region public operations

        public T GetMultiPartReply<T>(string operationName, params QueryStringParameter[] parameters) where T : Reply
        {
            throw new NotImplementedException();
        }

        public T GetReply<T>(string operationName, params QueryStringParameter[] parameters) where T : Reply
        {
            var url = BuildOperationUrl(operationName, parameters);

            var json = _webClient.DownloadString(url);

            T result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
        #endregion
    }
}
