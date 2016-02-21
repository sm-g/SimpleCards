using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleCards.Engine;
using System.Collections.Generic;

namespace SimpleCards.Tests
{
    [TestClass]
    public class TestCard
    {
        [TestMethod]
        public void TestCardsEquality()
        {
            SuitSet suitset = SuitSet.From<FrenchSuits>();
            RankSet rankset = RankSet.From<DefaultRanks>(r => (int)r);

            Card a = new Card(rankset[0], suitset[0]);
            Card asA = new Card(rankset[0], suitset[0]);
            Card b = new Card(rankset[0], suitset[1]);
            Card c = new Card(rankset[1], suitset[0]);
            Card d = new Card(rankset[1], suitset.GetSuit("Clubs").Value);

            Assert.AreEqual(a, a);
            Assert.AreEqual(a, asA);
            Assert.IsFalse(a == asA);
            Assert.IsTrue(a != b);
            Assert.IsTrue(a != c);
            Assert.IsTrue(b != c);

            var x = SuitSet.From<FrenchSuits>();
        }
    }
}
