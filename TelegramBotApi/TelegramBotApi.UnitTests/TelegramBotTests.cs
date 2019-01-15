using nerderies.TelegramBotApi;
using NUnit.Framework;
using System;
using TelegramBotApi;

namespace TelegramBotApi.UnitTests
{
    public class TelegramBotTests
    {
        [Test]
        public void Constructor_NullParameter_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new TelegramBot(null));
        }

        [Test]
        public void GetBot_NullParameter_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => TelegramBot.GetBot(null));
        }
            
    }
}