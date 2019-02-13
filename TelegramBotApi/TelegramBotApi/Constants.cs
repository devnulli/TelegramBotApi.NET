namespace nerderies.TelegramBotApi
{
    internal class Constants
    {
        internal const string ApiUrl = "https://api.telegram.org/bot%%token%%";
        internal const string FileUrl = "https://api.telegram.org/file/bot%%token%%/%%filepath%%";

        internal const int RequestLimit = 100;
        internal const int MaxTextLength = 1000;
        internal static readonly int DefaultRateLimitingTimeInMilliSeconds = 500;
    }
}
