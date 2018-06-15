using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class RankTests
    {
        [Test]
        public void EqualsByAllProps()
        {
            var a = new Rank("1", 1, false);
            var sameAsA = new Rank("1", 1, false);
            var otherFace = new Rank("1", 1, true);
            var otherName = new Rank("2", 1, false);
            var otherValue = new Rank("1", 2, false);

            Assert.IsTrue(a.Equals(sameAsA), "same");
            Assert.IsFalse(a.Equals(otherFace), "face");
            Assert.IsFalse(a.Equals(otherName), "name");
            Assert.IsFalse(a.Equals(otherValue), "val");
        }

        [Test]
        public void ComparesByValue()
        {
            var a = new Rank("1", 1, false);
            var sameAsA = new Rank("1", 1, false);
            var otherFace = new Rank("1", 1, true);
            var otherName = new Rank("2", 1, false);
            var greaterValue = new Rank("1", 2, false);
            var lesserValue = new Rank("1", 0, false);

            Assert.AreEqual(0, a.CompareTo(otherFace), "face");
            Assert.AreEqual(0, a.CompareTo(otherName), "name");
            Assert.AreEqual(-1, a.CompareTo(greaterValue), "great");
            Assert.AreEqual(1, a.CompareTo(lesserValue), "less");

            Assert.IsTrue(a > lesserValue, "gt lesser");
            Assert.IsTrue(a >= lesserValue, "gte lesser");
            Assert.IsTrue(a >= sameAsA, "gte same");
            Assert.IsTrue(a < greaterValue, "lt greater");
            Assert.IsTrue(a <= greaterValue, "lte greaterValue");
            Assert.IsTrue(a <= sameAsA, "lte same");

            Assert.IsTrue(a == sameAsA, "eq same");
            Assert.IsTrue(a == otherFace, "eq otherFace");
            Assert.IsTrue(a == otherName, "eq otherName");
            Assert.IsTrue(a != greaterValue, "neq other value");
        }

        [Test]
        public void ComparesByValue_NullCases()
        {
            var a = new Rank("1", 1, false);

            Assert.IsTrue(a > null, "gt null");
            Assert.IsTrue(a >= null, "gte null");
            Assert.IsFalse(a < null, "lt null");
            Assert.IsFalse(a <= null, "lte null");

#pragma warning disable SA1131 // Use readable conditions
            Assert.IsFalse(null > a, "null gt");
            Assert.IsFalse(null >= a, "null gte");
            Assert.IsTrue(null < a, "null lt");
            Assert.IsTrue(null <= a, "null lte");

            Assert.IsTrue(null != a, "null eq");
            Assert.IsTrue(a != null, "neq null");

#pragma warning restore SA1131 // Use readable conditions
        }
    }
}