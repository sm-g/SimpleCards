using System;

using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class RankSetTests
    {
        [Test]
        public void Ctor_RanksWithSameProps_Throws()
        {
            Assert.Catch<ArgumentException>(() => new RankSet(new[] { new Rank("1", 1), new Rank("1", 1) }));
        }

        [Test]
        public void Ctor_RanksWithSameValues_Throws()
        {
            Assert.Catch<ArgumentException>(() => new RankSet(new[] { new Rank("1", 1), new Rank("222", 1) }));
        }

        [Test]
        public void Ctor_RanksWithSameNames_Throws()
        {
            Assert.Catch<ArgumentException>(() => new RankSet(new[] { new Rank("1", 1), new Rank("1", 222) }));
        }

        [Test]
        public void Get_rank_by_name_returns_ignoring_case()
        {
            var rank = new Rank("king", 13);
            var set = new RankSet(new[] { rank });

            var result = set["King"];

            Assert.AreSame(result, rank);
        }

        [Test]
        public void Get_rank_by_name_returns_null()
        {
            var rank = new Rank("king", 13);
            var set = new RankSet(new[] { rank });

            var result = set["ace"];

            Assert.IsNull(result);
        }
    }
}