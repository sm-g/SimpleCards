using System;
using System.Linq;
using NUnit.Framework;
using SimpleCards.Engine;

namespace SimpleCards.Tests
{
    [TestFixture]
    public class PileTests
    {
        [Test]
        public void PushMiddle_NeverTopOrBottom()
        {
            var pile = new Pile(new[] { RndCard(), RndCard(), RndCard() });
            var card = RndCard();
            pile.PushMiddle(card);

            Assert.IsTrue(pile.First() != card);
            Assert.IsTrue(pile.Last() != card);
        }

        [Test]
        public void PushMiddle_TwoInPile()
        {
            var pile = new Pile(new[] { RndCard(), RndCard() });
            var card = RndCard();
            pile.PushMiddle(card);

            Assert.IsTrue(pile.ElementAt(1) == card);
        }

        [Test]
        public void PushMiddle_OneInPile_AddsToTop()
        {
            var pile = new Pile(new[] { RndCard() });
            var card = RndCard();
            pile.PushMiddle(card);

            Assert.IsTrue(pile.ElementAt(1) == card);
        }

        static Card RndCard()
        {
            return new Card(new Rank("1", 1), new Suit("1"));
        }
    }
}