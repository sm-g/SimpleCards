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
            SuitSet suitset = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Clubs || s == FrenchSuits.Diamonds ? 1 : 0);
            RankSet rankset = RankSet.From<DefaultRanks>(r => (int)r, new DefaultRanks[0]);

            Card a = new Card(rankset[0], suitset[0]);
            Card asA = new Card(rankset[0], suitset[0]);
            Card b = new Card(rankset[0], suitset[1]);
            Card c = new Card(rankset[1], suitset[0]);
            Card d = new Card(rankset[1], suitset.GetSuit("Clubs").ValueOrFailure());

            Assert.AreEqual(a, a);
            Assert.AreEqual(a, asA);
            Assert.IsFalse(a == asA);
            Assert.IsTrue(a != b);
            Assert.IsTrue(a != c);
            Assert.IsTrue(b != c);
        }
    }
}