using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBotApi.UnitTests
{
    public class QueryStringParameterTests
    {
        [Test]
        public void Constructor_NullParameter_Throws()
        {
            Assert.Throws<ArgumentException>(() => new QueryStringParameter(null, "A"));
            Assert.Throws<ArgumentException>(() => new QueryStringParameter("A",null));
            Assert.Throws<ArgumentException>(() => new QueryStringParameter(null, null));
            Assert.DoesNotThrow(() => new QueryStringParameter("A", "B"));
        }
    }
}
