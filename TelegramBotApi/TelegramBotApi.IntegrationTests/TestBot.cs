using nerderies.TelegramBotApi;
using nerderies.TelegramBotApi.DTOS;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Tests
{
    public class TestBot
    {
        private TelegramBot b = null;
        private Message _testMessage = null;

        [SetUp]
        public void Setup()
        {
            string token = null;
            DirectoryInfo documentsPath = null;
            try
            {
                documentsPath = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                var fileInfo = documentsPath.GetFiles("*.testtoken").First();
                token = File.ReadAllText(fileInfo.FullName).Trim();
                b = TelegramBot.GetBot(token);
                var updates = b.GetUpdates();
                _testMessage = updates.First();

            }
            catch(Exception)
            {
                throw new Exception($"You must first set up a test bot for integration testing and make sure that there is a *.testtoken file in {documentsPath}\rAlso, the bot should have at least one message in his backlog");
            }
            
        }

        [Test]
        public void GetMe_Returns_User()
        {
            var u = b.GetMe();
            Assert.IsNotNull(u);
            Assert.IsNotNull(u.FirstName);
            Assert.IsNotNull(u.Username);
            Assert.That(u.Id > 0);
            Assert.IsTrue(u.IsBot);  
        }

        [Test]
        public void SendMessage_Basic_Returns()
        {
            b.SendMessage(_testMessage.Chat.Id, "SendMessage_Basic_Returns");
        }
    }
}
