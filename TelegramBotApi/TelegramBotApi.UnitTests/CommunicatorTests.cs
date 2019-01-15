using nerderies.TelegramBotApi;
using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TelegramBotAPI.UnitTests")]
namespace TelegramBotApi.UnitTests
{   
    public class CommunicatorTests
    {
        [Test]
        public void Constructor_NullParameter_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Communicator(null, Constants.DefaultRateLimitingTimeInMilliSeconds));
        }
    }
}
