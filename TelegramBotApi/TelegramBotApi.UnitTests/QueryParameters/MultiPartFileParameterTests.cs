using NUnit.Framework;
using System;

namespace nerderies.TelegramBotApi.UnitTests.Parameters
{
    public class MultiPartFileParameterTests
    {
        [Test]
        public void Constructor_NullArgument_Throws()
        {
            Assert.Throws<ArgumentNullException>(()=>new MultiPartFileParameter(null, "dummy", new byte[] { 1 }, "dummy"));
            Assert.Throws<ArgumentNullException>(() => new MultiPartFileParameter("dummy", null, new byte[] { 1 }, "dummy"));
            Assert.Throws<ArgumentNullException>(() => new MultiPartFileParameter("dummy", "dummy", null, "dummy"));
            Assert.Throws<ArgumentNullException>(() => new MultiPartFileParameter("dummy", "dummy", new byte[] { 1 }, null));
        }

        [Test]
        public void Constructor_EmptyContent_Throws()
        {
            Assert.Throws<ArgumentException>(() => new MultiPartFileParameter("dummy", "dummy", new byte[0] { }, "dummy"));
        }
    }
}
