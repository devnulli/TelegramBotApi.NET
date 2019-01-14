using System;
using System.Net;
using nerderies.TelegramBotApi.DTOS;
using nerderies.TelegramBotApi.Interfaces;
using Newtonsoft.Json;

namespace nerderies.TelegramBotApi
{
    internal class Communicator : ICommunicator
    {
        #region private members

        private string _authenticationToken = null;
        private WebClient _webClient = new WebClient();
        private long _rateLimitingMilliSeconds = 0;
        private DateTime _lastRequest = DateTime.MinValue;

        #endregion

        #region .ctor

        public Communicator(string authenticationToken, long rateLimitingMilliSeconds)
        {
            _authenticationToken = authenticationToken;
            _rateLimitingMilliSeconds = rateLimitingMilliSeconds;
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

        private string GetRateLimitedReply(string url)
        {
            lock(this)
            {
                while(_rateLimitingMilliSeconds != 0 && DateTime.Now < _lastRequest.AddMilliseconds(_rateLimitingMilliSeconds))
                {
                    System.Threading.Thread.Sleep(10);
                }
                _lastRequest = DateTime.Now;
                return _webClient.DownloadString(url);
            }
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

            var json = GetRateLimitedReply(url);

            T result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
        #endregion
    }
}
