using nerderies.TelegramBotApi.DTOS;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Net;

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
            catch
            {
                throw new Exception($"You must first (A) set up a test bot for integration testing (B) make sure that there is a *.testtoken file containing the bots token in {documentsPath} (C) The bot must have at least one message in his backlog");
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

        [Test]
        public void SendVoice_Returns()
        {
            var m1 = _b.SendVoice(_testMessage.Chat, new TelegramFile(_testobjects.TestVoice, "test.ogg", "audio/ogg"));
            Assert.NotNull(m1);
            Assert.NotNull(m1.Voice);
        }

        [Test]
        public void SendVideoNote_Returns()
        {
            var m1 = _b.SendVideoNote(_testMessage.Chat, new TelegramFile(_testobjects.TestVideo, "videoNote", "video/mp4"));
            Assert.NotNull(m1);

            //strangely, videoNote remains empty and the message is stored in the video property..
            Assert.NotNull(m1.Video);
        }

        [Test]
        public void SendLocation_Returns()
        {
            var m1 = _b.SendLocation(_testMessage.Chat, new Location() { Latitude = 48.305859, Longitude = 14.286459 }, replyToMessage: _testMessage);
            Assert.NotNull(m1);
            Assert.NotNull(m1.Location);
        }

        [Test]
        public void LiveLocation_Returns_HasWorkingTimer()
        {
            var originalMessage = _b.SendLocation(_testMessage.Chat, new Location() { Latitude = 48.305859, Longitude = 14.286459 }, livePeriod: 60);
            Assert.NotNull(originalMessage);

            for (int i = 0; i < 5; i++)
            {
                System.Threading.Thread.Sleep(10000);
                originalMessage = _b.EditMessageLiveLocation(originalMessage, new Location() { Latitude = originalMessage.Location.Latitude + 0.001, Longitude = originalMessage.Location.Longitude + 0.001 });
                Assert.NotNull(originalMessage);
            }

            System.Threading.Thread.Sleep(30000);
            Assert.Throws<WebException>(() => _b.EditMessageLiveLocation(originalMessage, new Location() { Latitude = originalMessage.Location.Latitude + 0.001, Longitude = originalMessage.Location.Longitude + 0.001 }));

            var abortLiveLocation = _b.SendLocation(_testMessage.Chat, new Location() { Latitude = 48.305859, Longitude = 14.286459 }, livePeriod: 60);
            Assert.NotNull(abortLiveLocation);
            abortLiveLocation = _b.StopMessageLiveLocation(abortLiveLocation);
            Assert.NotNull(abortLiveLocation);
            System.Threading.Thread.Sleep(1000);
            Assert.Throws<WebException>(() => _b.EditMessageLiveLocation(abortLiveLocation, new Location() { Latitude = originalMessage.Location.Latitude + 0.001, Longitude = originalMessage.Location.Longitude + 0.001 }));
        }

        [Test]
        public void SendVenue_Returns()
        {
            var title = "Steel City";
            var address = "Hauptplatz, Linz, Austria";
            var m1 = _b.SendVenue(_testMessage.Chat, new Location() { Latitude = 48.305859, Longitude = 14.286459 }, title: title, address: address);
            Assert.NotNull(m1);
            Assert.NotNull(m1.Venue);
            Assert.NotNull(m1.Venue.Title);
            Assert.NotNull(m1.Venue.Address);
            Assert.That(m1.Venue.Title == title);
            Assert.That(m1.Venue.Address==address);
            Assert.That(string.IsNullOrEmpty(m1.Venue.FoursquareType));

            title = "Amici";
            address = "Verlängerte Kirchengasse, 4040 Linz, Österreich";
            var foursquareId = "4b6076b0f964a52082e729e3";
            var foursquareType = "4bf58dd8d48988d1ca941735"; //pizzeria
            m1 = _b.SendVenue(_testMessage.Chat, new Location() { Latitude = 48.312400, Longitude = 14.287152 }, title: title, address: address, foursquareId: foursquareId, foursquareType: foursquareType);
            Assert.NotNull(m1);
            Assert.NotNull(m1.Venue);
            Assert.NotNull(m1.Venue.Title);
            Assert.NotNull(m1.Venue.Address);
            Assert.NotNull(m1.Venue.FoursquareId);
            Assert.NotNull(m1.Venue.FoursquareType);
            Assert.That(m1.Venue.Title == title);
            Assert.That(m1.Venue.Address == address);
            Assert.That(m1.Venue.FoursquareId == foursquareId);
            Assert.That(m1.Venue.FoursquareType == foursquareType);
        }

        [Test]
        public void SendContact_Returns()
        {
            var m1 = _b.SendContact(_testMessage.Chat, "+43 677 626 91526", "Mike");
            Assert.NotNull(m1);
            Assert.NotNull(m1.Contact);
            Assert.NotNull(m1.Contact.PhoneNumber);
            Assert.NotNull(m1.Contact.FirstName);

            m1 = _b.SendContact(_testMessage.Chat, "+43 677 62691526", "Mike", lastName: "Schönbauer", replyToMessage: _testMessage);
            Assert.NotNull(m1);
            Assert.NotNull(m1.Contact);
            Assert.NotNull(m1.Contact.LastName);
            Assert.NotNull(m1.ReplyToMessage);
        }

        [Test]
        public void GetUserProfilePhotos_Returns()
        {
            User testUser = new User() { Id = 20793245 };
            var m1 = _b.GetUserProfilePhotos(testUser);
            Assert.NotNull(m1);

            var count = m1.Count;

            //when the test user has more than 3 pics, we can additionally test the limit parameter
            if (m1.Count > 3)
            {
                var m2 = _b.GetUserProfilePhotos(testUser, 3);
                Assert.NotNull(m2);
                Assert.That(m2.Count == 3);
            }

            int cnt = 0;
            foreach(var x in m1)
            {
                _b.SendPhoto(_testMessage.Chat, new TelegramFile(x[0].FileId), caption: $"Photo {cnt}:{x[0].FileId}");
                cnt++;
                if (cnt > 4)
                    break;
            }
        }

        [Test]
        public void GetFile_Returns()
        {
            User testUser = new User() { Id = 20793245 };
            var m1 = _b.GetUserProfilePhotos(testUser);
            if(m1.Count > 0)
            {
                var result = _b.GetFile(m1[0][0].FileId);
                Assert.NotNull(result);
                Assert.NotNull(result.FileId);
                Assert.NotNull(result.FilePath);
                Assert.NotNull(result.FileSize);
            }
        }
    }
}
