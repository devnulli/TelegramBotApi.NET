using NUnit.Framework;
using System.IO;
using TelegramBotApi;

namespace Tests
{
    public class TestBot
    {
        private TelegramBot b = null;

        [SetUp]
        public void Setup()
        {
            b = TelegramBot.GetBot("700338044:AAErW5RbuZNayYPR11fOBnbn1DM1qwuj4ss");
        }

        [Test]
        public void Communicator_GetUpdates_Returns()
        {
            var x = b.GetUpdates();
            Assert.DoesNotThrow(() => b.GetUpdates());
        }
    }
}