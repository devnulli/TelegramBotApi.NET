using NUnit.Framework;
using System;

namespace nerderies.TelegramBotApi.UnitTests.Parameters
{
    public class QueryStringParameterTests
    {
        [Test]
        public void Constructor_NullParameter_Throws()
        {
            Assert.Throws<ArgumentException>(() => new QueryStringParameter(null, "A"));
            Assert.Throws<ArgumentException>(() => new QueryStringParameter("A",(string[])null));
            Assert.Throws<ArgumentException>(() => new QueryStringParameter(null, (string)null));
        }
    }
}
