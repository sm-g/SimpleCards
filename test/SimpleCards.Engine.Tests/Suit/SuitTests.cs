using NUnit.Framework;
using Optional.Unsafe;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class SuitTests
    {
        [Test]
        public void EqualsByAllProps()
        {
            var a = new Suit("s1", 1);
            var sameAsA = new Suit("s1", 1);
            var otherName = new Suit("2", 1);
            var otherColor = new Suit("2", 2);

            Assert.IsTrue(a.Equals(sameAsA), "same");
            Assert.IsTrue(a == sameAsA, "==");
            Assert.IsFalse(a.Equals(otherName), "name");
            Assert.IsFalse(a.Equals(otherColor), "val");

            Assert.IsFalse(a == ((Suit)null));
        }
    }
}