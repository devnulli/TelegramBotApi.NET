using nerderies.TelegramBotApi;
using nerderies.TelegramBotApi.DTOS;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace nerderies.TelegramBotApi.IntegrationTests
{
    public class TestBot
    {
        private TelegramBot _b = null;
        private Message _testMessage = null;
        private TestObjects _testobjects = null;

        [SetUp]
        public void Setup()
        {
            string token = null;
            DirectoryInfo documentsPath = null;
            string json = File.ReadAllText("testobjects.json");
            _testobjects = JsonConvert.DeserializeObject<TestObjects>(json);

            try
            {
                documentsPath = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Personal));
                var fileInfo = documentsPath.GetFiles("*.testtoken").First();
                token = File.ReadAllText(fileInfo.FullName).Trim();
                _b = TelegramBot.GetBot(token, true);
                var updates = _b.GetUpdates();
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
            var u = _b.GetMe();
            Assert.IsNotNull(u);
            Assert.IsNotNull(u.FirstName);
            Assert.IsNotNull(u.Username);
            Assert.That(u.Id > 0);
            Assert.IsTrue(u.IsBot);  
        }

        [Test]
        public void SendMessage_Basic_Returns()
        {
            Assert.NotNull(_b.SendMessage(_testMessage.Chat, "SendMessage_Basic_Returns") != null);
        }

        [Test]
        public void SendMessage_ReplyTo_Returns()
        {
            Assert.NotNull(_b.SendMessage(_testMessage.Chat, "SendMessage_ReplyTo_Returns", replyToMessage: _testMessage));
        }

        [Test]
        public void SendMessage_HTML_Returns()
        {
            string testText = "<b>bold</b>, <strong>bold</strong>\n" +
                "<i>italic</i>, <em>italic</em>\n" +
                "<a href=\"http://www.example.com/\">inline URL</a>\n" +
                $"<a href=\"tg://user?id={_testMessage.From.Id}\">inline mention of a user</a>\n" +
                "<code>inline fixed-width code</code>\n" +
                "<pre>pre-formatted fixed-width code block</pre>";

            Assert.NotNull(_b.SendMessage(_testMessage.Chat, testText, TelegramBot.MarkdownStyles.HTML));
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

            Assert.NotNull(_b.SendMessage(_testMessage.Chat, testText, TelegramBot.MarkdownStyles.Markdown));
        }

        [Test]
        public void SendMessage_DisableWeb_Page_Preview_Returns()
        {
            Assert.NotNull(_b.SendMessage(_testMessage.Chat, "no preview: check this out http://www.derstandard.at", disableWebPagePreview: true));
            Assert.NotNull(_b.SendMessage(_testMessage.Chat, "preview: check this out http://www.derstandard.at", disableWebPagePreview: false));
        }

        [Test]
        public void ForwardMessage_Returns()
        {
            Assert.NotNull(_b.ForwardMessage(_testMessage, _testMessage.Chat));
            Assert.NotNull(_b.ForwardMessage(_testMessage, _testMessage.Chat, disableNotification: true));
        }

        [Test]
        public void SendPhoto_Returns()
        {
            string photo = "\"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\"";
            byte[] bytes = JsonConvert.DeserializeObject<byte[]>(photo);

            var message = _b.SendPhoto(_testMessage.Chat, new TelegramFile(bytes, "testimage", "img/gif"), caption: "caption", replyToMessage: _testMessage);
            Assert.NotNull(message);
            Assert.NotNull(message.Photos);
            Assert.NotNull(message.ReplyToMessage);
            Assert.That(message.Photos.Length > 0);
            var message2 = _b.SendPhoto(_testMessage.Chat, new TelegramFile(message.Photos[0].FileId), caption:"*echo with markdown*", markdownStyle: TelegramBot.MarkdownStyles.Markdown);
            Assert.NotNull(message2);
            Assert.Null(message2.ReplyToMessage);
            Assert.That(message2.Photos.Length > 0);
        }

        [Test]
        public void SendChatAction_Returns()
        {
            Assert.True(_b.SendChatAction(_testMessage.Chat, ChatAction.UploadDocument));
        }

        [Test]
        public void SendAudio_Returns()
        {
            var m1 = _b.SendAudio(_testMessage.Chat, new TelegramFile(_testobjects.TestAudio, "test", "audio/mpeg"), thumb: new TelegramFile(_testobjects.TestThumb, "thumb", "img/jpeg"), performer: "cat", title: "testaudio");
            Assert.NotNull(m1);
        }

        [Test]
        public void SendDocument_Returns()
        {
            var m1 = _b.SendDocument(_testMessage.Chat, new TelegramFile(_testobjects.TestAudio, "testdocument", "audio/mpeg"), thumb: new TelegramFile(_testobjects.TestThumb, "thumb", "img/jpeg"));
            Assert.NotNull(m1);
            var m2 = _b.SendDocument(_testMessage.Chat, new TelegramFile(m1.Document.FileId), thumb: new TelegramFile(m1.Document.Thumb.FileId));
        }
    }
}
