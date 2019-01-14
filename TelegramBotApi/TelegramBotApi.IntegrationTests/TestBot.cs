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
                b = TelegramBot.GetBot(token, true);
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

        [Test]
        public void SendMessage_ReplyTo_Returns()
        {
            b.SendMessage(_testMessage.Chat.Id, "SendMessage_ReplyTo_Returns", _testMessage);
        }

        [Test]
        public void SendMessage_HTML_Returns()
        {
            string testText = "<b>bold</b>, <strong>bold</strong>\n" +
                "<i>italic</i>, <em>italic</em>\n" +
                "<a href=\"http://www.example.com/\">inline URL</a>\n" +
                "<a href=\"tg://user?id=123456789\">inline mention of a user</a>\n" +
                "<code>inline fixed-width code</code>\n" +
                "<pre>pre-formatted fixed-width code block</pre>";

            b.SendMessage(_testMessage.Chat.Id, testText, _testMessage, TelegramBot.MarkdownStyles.HTML);
        }

        [Test]
        public void SendMessage_Markdown_Returns()
        {
            string testText ="*bold text*\n" +
                "_italic text_\n" +
                "[inline URL](http://www.example.com/)\n" +
                $"[inline mention of a user](tg://user?id={_testMessage.From.Id})\n" +
                "`inline fixed-width code`\n" +
                "```block_language\n" +
                "pre-formatted fixed-width code block ```";

            b.SendMessage(_testMessage.Chat.Id, testText, _testMessage, TelegramBot.MarkdownStyles.Markdown);
        }
    }
}
