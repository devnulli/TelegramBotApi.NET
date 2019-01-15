using NUnit.Framework;
using System;

namespace nerderies.TelegramBotApi.UnitTests
{
    public class MultipartStringParameterTests
    {
        [Test]
        public void Constructor_NullParameter_Throws()
        {
            Assert.Throws<ArgumentException>(() => new MultiPartStringParameter(null, "A"));
            Assert.Throws<ArgumentException>(() => new MultiPartStringParameter("A", null));
            Assert.Throws<ArgumentException>(() => new MultiPartStringParameter(null, null));
        }
    }
}
