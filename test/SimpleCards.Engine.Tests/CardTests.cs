using System;
using System.Linq;
using NUnit.Framework;
using SimpleCards.Engine;

namespace SimpleCards.Tests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void EqualsByRankAndSuit()
        {
            var x = new Card(new Rank("r", 1), new Suit("s"));
            var y = new Card(new Rank("r", 1), new Suit("s"));

            Assert.AreEqual(x, y);
        }
    }
}