using System;
using NUnit.Framework;
using Optional.Unsafe;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class SuitSetTests
    {
        [Test]
        public void Ctor_SuitsWithSameProps_Throws()
        {
            Assert.Catch<ArgumentException>(() => new SuitSet(new[] { new Suit("1", 1), new Suit("1", 1) }));
        }

        [Test]
        public void Ctor_SuitsWithSameNames_Throws()
        {
            Assert.Catch<ArgumentException>(() => new SuitSet(new[] { new Suit("1", 1), new Suit("1", 222) }));
        }

        [Test]
        public void GetSuit_returnsByName()
        {
            var suit = new Suit("s1");
            var set = new SuitSet(new[] { suit });

            var res = set.GetSuit("s1").ValueOrFailure();

            Assert.AreSame(res, suit);
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