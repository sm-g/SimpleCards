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

            pile.Push(card, PilePosition.Middle);

            Assert.IsTrue(pile.First() != card);
            Assert.IsTrue(pile.Last() != card);
        }

        [Test]
        public void PushMiddle_TwoInPile()
        {
            var pile = new Pile(new[] { RndCard(), RndCard() });
            var card = RndCard();

            pile.Push(card, PilePosition.Middle);

            Assert.IsTrue(pile.ElementAt(1) == card);
        }

        [Test]
        public void PushMiddle_OneInPile_AddsToTop()
        {
            var pile = new Pile(new[] { RndCard() });
            var card = RndCard();

            pile.Push(card, PilePosition.Middle);

            Assert.IsTrue(pile.ElementAt(1) == card);
        }

        [Test]
        public void PopMiddle_TakesOrderedGroup()
        {
            var pile = new Pile(new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) });

            var res = pile.Pop(PilePosition.Middle, 3);

            CollectionAssert.IsOrdered(res, new Card.RankValueComparer());
            Assert.IsTrue(pile.Size == 1);
            Assert.IsTrue(pile.First().Rank.Value == 1 || pile.First().Rank.Value == 4);
        }

        private static Card RndCard(int i = 1)
        {
            return new Card(new Rank("1", i), new Suit("1"));
        }
    }
}