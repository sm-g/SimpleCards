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
            var suitSet = new SuitSet(new[] { new Suit("s1", Color.Black), new Suit("s2", Color.Black) });
            var rankSet = new RankSet(new[] { new Rank("r1", 1), new Rank("r2", 2, true) });

            var pack = new Pack(suitSet, rankSet, shuffle: false);

            Assert.AreEqual(suitSet.Count * rankSet.Count, pack.Size);
            CollectionAssert.AreEqual(rankSet, pack.Where(x => x.Suit == suitSet[0]).Select(x => x.Rank));
            CollectionAssert.AreEqual(suitSet, pack.Where(x => x.Rank == rankSet[0]).Select(x => x.Suit));
        }

        [Test]
        public void Ctor_WhenManyDecks_MakesPileWithAllPossibleCardsOrdered()
        {
            var suitSet = new SuitSet(new[] { new Suit("s1", Color.Black), new Suit("s2", Color.Black) });
            var rankSet = new RankSet(new[] { new Rank("r1", 1), new Rank("r2", 2, true) });

            var pack = new Pack(suitSet, rankSet, shuffle: false, decksCount: 2);

            Assert.AreEqual(suitSet.Count * rankSet.Count * 2, pack.Size);
            CollectionAssert.AreEqual(new[] {
                new Card(rankSet[0], suitSet[0]),
                new Card(rankSet[0], suitSet[0]),
                new Card(rankSet[1], suitSet[0]),
                new Card(rankSet[1], suitSet[0]),
                new Card(rankSet[0], suitSet[1])
            }, pack.Take(5));
        }

        [Test]
        [Retry(3)] // due to random of shuffle
        public void Ctor_WhenShuffle_MakesPileWithAllPossibleCardsShuffled()
        {
            var suitSet = new SuitSet(new[] { new Suit("s1", Color.Black), new Suit("s2", Color.Black) });
            var rankSet = new RankSet(new[] { new Rank("r1", 1), new Rank("r2", 2, true) });

            var pack = new Pack(suitSet, rankSet, shuffle: true);

            Assert.AreEqual(suitSet.Count * rankSet.Count, pack.Size);
            CollectionAssert.AreNotEqual(new[] {
                new Card(rankSet[0], suitSet[0]),
                new Card(rankSet[0], suitSet[0]),
                new Card(rankSet[1], suitSet[0]),
                new Card(rankSet[1], suitSet[0]),
                new Card(rankSet[0], suitSet[1])
            }, pack.Take(5));
        }
    }
}