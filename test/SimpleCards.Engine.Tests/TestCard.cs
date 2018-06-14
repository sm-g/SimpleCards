using NUnit.Framework;
using Optional.Unsafe;
using SimpleCards.Engine;

namespace SimpleCards.Tests
{
    [TestFixture]
    public class TestCard
    {
        [Test]
        public void TestCardsEquality()
        {
            var suitset = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Clubs || s == FrenchSuits.Diamonds ? 1 : 0);
            var rankset = RankSet.From<DefaultRanks>(r => (int)r, new DefaultRanks[0]);

            var a = new Card(rankset[0], suitset[0]);
            var asA = new Card(rankset[0], suitset[0]);
            var b = new Card(rankset[0], suitset[1]);
            var c = new Card(rankset[1], suitset[0]);
            var d = new Card(rankset[1], suitset.GetSuit("Clubs").ValueOrFailure());

            Assert.AreEqual(a, a);
            Assert.AreEqual(a, asA);
            Assert.IsFalse(a == asA);
            Assert.IsTrue(a != b);
            Assert.IsTrue(a != c);
            Assert.IsTrue(b != c);
        }
    }
}