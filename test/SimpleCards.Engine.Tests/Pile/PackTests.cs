using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class PackTests
    {
        [Test]
        public void Ctor_MakesPileWithAllPossibleCardsOrdered()
        {
            var suitSet = new SuitSet(new[] { new Suit("s1"), new Suit("s2") });
            var rankSet = new RankSet(new[] { new Rank("r1", 1), new Rank("r2", 2, true) });

            var pack = new Pack(suitSet, rankSet, shuffle: false);

            Assert.AreEqual(suitSet.Count * rankSet.Count, pack.Size);
            CollectionAssert.IsOrdered(pack.Where(x => x.Suit == suitSet[0]), CardByRankValueComparer.Instance);
        }

        [Test]
        public void Ctor_WhenManyDecks_MakesPileWithAllPossibleCardsOrdered()
        {
            var suitSet = new SuitSet(new[] { new Suit("s1"), new Suit("s2") });
            var rankSet = RankSet.From<DefaultRanks>(r => (int)r, new[] { DefaultRanks.Jack, DefaultRanks.Queen, DefaultRanks.King });

            var pack = new Pack(suitSet, rankSet, shuffle: false, decksCount: 3);

            Assert.AreEqual(suitSet.Count * rankSet.Count * 3, pack.Size);
            CollectionAssert.IsOrdered(pack.Where(x => x.Suit == suitSet[0]), CardByRankValueComparer.Instance);
        }

        [Test]
        public void Ctor_WhenShuffle_MakesPileWithAllPossibleCardsShuffled()
        {
            var suitSet = new SuitSet(new[] { new Suit("s1"), new Suit("s2") });
            var rankSet = RankSet.From<DefaultRanks>(r => (int)r, new[] { DefaultRanks.Jack, DefaultRanks.Queen, DefaultRanks.King });

            var pack = new Pack(suitSet, rankSet, shuffle: true);

            Assert.AreEqual(suitSet.Count * rankSet.Count, pack.Size);
            Assert.IsFalse(pack.Where(x => x.Suit == suitSet[0]).IsOrdered(x => x.Rank));
        }
    }
}