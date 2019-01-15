using NUnit.Framework;
using System;

namespace nerderies.TelegramBotApi.UnitTests
{
    public class CommunicatorTests
    {
        [Test]
        public void Constructor_NullParameter_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new Communicator(null, Constants.DefaultRateLimitingTimeInMilliSeconds));
        }

        [Test]
        public void Constructor_NegativeRateLimiting_Throws()
        {
            Assert.Throws<ArgumentException>(() => new Communicator("Sometoken", -20));
        }
    }
}
