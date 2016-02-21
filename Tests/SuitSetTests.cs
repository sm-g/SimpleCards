using System;
using System.Linq;
using NUnit.Framework;
using SimpleCards.Engine;

namespace SimpleCards.Tests
{
    [TestFixture]
    public class SuitSetTests
    {
        [Test]
        public void GetSuit_returnsByName()
        {
            var suit = new Suit("s1");
            var set = new SuitSet(new[] { suit });

            var res = set.GetSuit("s1").Value;

            Assert.AreEqual(res, suit);
        }

        [Test]
        public void GetSuit_returnsByName_nothing()
        {
            var suit = new Suit("s1");
            var set = new SuitSet(new[] { suit });

            var res = set.GetSuit("s2").HasValue;

            Assert.AreEqual(false, res);
        }
    }
}