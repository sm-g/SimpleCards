using NUnit.Framework;

namespace SimpleCards.Engine
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

        [Test]
        public void CardsEquality()
        {
            var suitset = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Clubs || s == FrenchSuits.Diamonds ? 1 : 0);
            var rankset = RankSet.From<DefaultRanks>(r => (int)r, new DefaultRanks[0]);

            var a = new Card(rankset[0], suitset[0]);
            var sameAsA = new Card(rankset[0], suitset[0]);
            var b = new Card(rankset[0], suitset[1]);
            var c = new Card(rankset[1], suitset[0]);

            Assert.AreEqual(a, a);
            Assert.AreEqual(a, sameAsA);
            Assert.IsTrue(a == sameAsA);

            Assert.AreNotEqual(a, b);
            Assert.AreNotEqual(a, c);
            Assert.AreNotEqual(b, c);
            Assert.IsTrue(a != b);
            Assert.IsTrue(a != c);
            Assert.IsTrue(b != c);
        }
    }
}