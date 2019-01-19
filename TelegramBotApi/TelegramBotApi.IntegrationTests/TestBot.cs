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
            catch(Exception e)
            {
                throw new Exception($"You must first (A) set up a test bot for integration testing (B) make sure that there is a *.testtoken file containing the bots token in {documentsPath} (C) The bot should have at least one message in his backlog");
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
            var r1 = _b.ForwardMessage(_testMessage, _testMessage.Chat);
            Assert.NotNull(r1);
            Assert.NotNull(r1.ForwardFrom);
        }

        [Test]
        public void SendPhoto_Returns()
        {
            byte[] bytes = _testobjects.TestPhoto;

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
            Assert.NotNull(m1.Audio);
        }

        [Test]
        public void SendDocument_Returns()
        {
            var m1 = _b.SendDocument(_testMessage.Chat, new TelegramFile(_testobjects.TestAudio, "testdocument", "audio/mpeg"), thumb: new TelegramFile(_testobjects.TestThumb, "thumb", "img/jpeg"));
            Assert.NotNull(m1);
            Assert.NotNull(m1.Document);
            var m2 = _b.SendDocument(_testMessage.Chat, new TelegramFile(m1.Document.FileId), thumb: new TelegramFile(m1.Document.Thumb.FileId));
            Assert.NotNull(m2);
            Assert.NotNull(m2.Document);
        }

        [Test]
        public void SendVideo_Returns()
        {
            var m1 = _b.SendVideo(_testMessage.Chat, new TelegramFile(_testobjects.TestVideo, "light.mp4", "video/mp4"), thumb:new TelegramFile(_testobjects.TestThumb, "testthumb", "img/jpeg"));
            Assert.NotNull(m1);
            Assert.NotNull(m1.Video);
        }

        [Test]
        public void SendAnimation_Returns()
        {
            var m1 = _b.SendAnimation(_testMessage.Chat, new TelegramFile(_testobjects.TestAnimation, "test.gif", "img/gif"));
            Assert.NotNull(m1);
            Assert.NotNull(m1.Animation);
        }
    }
}
