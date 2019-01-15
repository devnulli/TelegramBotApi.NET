using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using nerderies.TelegramBotApi.DTOS;
using nerderies.TelegramBotApi.Helpers;
using nerderies.TelegramBotApi.Interfaces;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("TelegramBotApi.UnitTests")]
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
            if (rateLimitingMilliSeconds < 0)
                throw new ArgumentException("rateLimiting cannot make the telegram bot work faster than light (it must be positive)");

            _webClient.Headers.Add(HttpRequestHeader.UserAgent, UserAgentString);
            _authenticationToken = authenticationToken ?? throw new ArgumentNullException();
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

        private string GetRateLimitedMultipartReply(string url, MultiPartParameter[] parameters)
        {
            lock (this)
            {
                RateLimit();

                using (var response = MultipartFormUploadHelper.MultipartFormDataPost(url, UserAgentString, parameters))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                            return reader.ReadToEnd();

                }
                    else throw new Exception("Multipartrequest returned an error");
                }
            }
        }

        private string GetRateLimitedReply(string url)
        {
            lock(this)
            {
                RateLimit();

                return _webClient.DownloadString(url);
            }
        }

        private void RateLimit()
        {
            while (_rateLimitingMilliSeconds != 0 && DateTime.Now < _lastRequest.AddMilliseconds(_rateLimitingMilliSeconds))
            {
                System.Threading.Thread.Sleep(10);
            }
            _lastRequest = DateTime.Now;
        }

        private string UserAgentString => $"nerderies.TelegramBotApi {Assembly.GetExecutingAssembly().GetName().Version}";
        #endregion

        #region public operations

        public T GetMultiPartReply<T>(string operationName, params MultiPartParameter[] parameters) where T : Reply
        {
            var url = BuildOperationUrl(operationName, null);

            var json = GetRateLimitedMultipartReply(url, parameters);

            return JsonConvert.DeserializeObject<T>(json);
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
