using System;
using System.Collections.Generic;
using System.Text;
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
    }
}