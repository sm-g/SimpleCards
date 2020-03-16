using System;

using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class SuitSetTests
    {
        [Test]
        public void Ctor_SuitsWithSameProps_Throws()
        {
            Assert.Catch<ArgumentException>(() => new SuitSet(new[]
            {
                new Suit("1", Color.Black),
                new Suit("1", Color.Black)
            }));
        }

        [Test]
        public void Ctor_SuitsWithSameNames_Throws()
        {
            Assert.Catch<ArgumentException>(() => new SuitSet(new[]
            {
                new Suit("1", Color.Black),
                new Suit("1", Color.Red)
            }));
        }

        [Test]
        public void Get_suit_by_name_returns_ignoring_case()
        {
            var suit = new Suit("s1", Color.Black);
            var set = new SuitSet(new[] { suit });

            var result = set["s1"];

            Assert.AreSame(result, suit);
        }

        [Test]
        public void Get_suit_by_name_returns_null()
        {
            var suit = new Suit("s1", Color.Black);
            var set = new SuitSet(new[] { suit });

            var result = set["s2"];

            Assert.IsNull(result);
        }
    }
}