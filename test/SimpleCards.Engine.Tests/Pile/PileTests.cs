using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SimpleCards.Engine;

namespace SimpleCards.Tests
{
    [TestFixture]
    public class PileTests
    {
        [Test]
        public void Ctor_UsesGivenCardsInGivenOrder()
        {
            var source = new[] { RndCard(3), RndCard(1), RndCard(3), RndCard(4) };

            var result = new Pile(source);

            Assert.IsTrue(result.Size == 4);
            Assert.AreSame(source[0], result.ElementAt(0), "0");
            Assert.AreSame(source[1], result.ElementAt(1), "1");
            Assert.AreSame(source[2], result.ElementAt(2), "2");
            Assert.AreSame(source[3], result.ElementAt(3), "3");
        }

        #region Push

        [Test]
        public void Push_AddsCard([Values] PilePosition position)
        {
            var pile = new Pile();
            var card = RndCard();

            pile.Push(card, position);

            Assert.AreEqual(1, pile.Size);
        }

        [Test]
        public void Push_Top_AddedCardIsFirst()
        {
            var pile = new Pile(new[] { RndCard(), RndCard(), RndCard() });
            var card = RndCard();

            pile.Push(card, PilePosition.Top);

            Assert.AreSame(card, pile.First());
        }

        [Test]
        public void Push_Top_EmptyPile_Ok()
        {
            var pile = new Pile();
            var card = RndCard();

            pile.Push(card, PilePosition.Top);

            Assert.AreSame(card, pile.First());
        }

        [Test]
        public void Push_Bottom_AddedCardIsLast()
        {
            var pile = new Pile(new[] { RndCard(), RndCard(), RndCard() });
            var card = RndCard();

            pile.Push(card, PilePosition.Bottom);

            Assert.AreSame(card, pile.Last());
        }

        [Test]
        public void Push_Bottom_EmptyPile_Ok()
        {
            var pile = new Pile();
            var card = RndCard();

            pile.Push(card, PilePosition.Bottom);

            Assert.AreSame(card, pile.First());
        }

        [Test]
        public void Push_Middle_AddedCardAlwaysInDifferentPlace()
        {
            var addedCard = RndCard();

            var dict = new Dictionary<int, int>();
            for (var i = 0; i < 50; i++)
            {
                var pile = new Pile(Enumerable.Repeat(RndCard(), 10));

                pile.Push(addedCard, PilePosition.Middle);

                var indexOfAddedCard = pile
                    .Select((card, index) => (card, index))
                    .Where(z => ReferenceEquals(z.card, addedCard))
                    .First()
                    .index;

                if (dict.ContainsKey(indexOfAddedCard))
                    dict[indexOfAddedCard]++;
                else
                    dict[indexOfAddedCard] = 1;
            }
            Assert.That(dict.Select(x => x.Value), Has.All.GreaterThan(1));
            Assert.IsFalse(dict.ContainsKey(0));
            Assert.IsFalse(dict.ContainsKey(10));
        }

        [Test]
        public void Push_Middle_TwoInPile_AddedCardAlwaysIsInMiddle()
        {
            var pile = new Pile(new[] { RndCard(), RndCard() });
            var card = RndCard();

            pile.Push(card, PilePosition.Middle);

            Assert.AreSame(pile.ElementAt(1), card);
            Assert.IsFalse(ReferenceEquals(pile.First(), card));
            Assert.IsFalse(ReferenceEquals(pile.Last(), card));
        }

        [Test]
        public void Push_Middle_OneInPile_AddedCardIsLast()
        {
            var pile = new Pile(new[] { RndCard() });
            var card = RndCard();

            pile.Push(card, PilePosition.Middle);

            Assert.AreSame(card, pile.Last());
        }

        [Test]
        public void Push_Middle_EmptyPile_Ok()
        {
            var pile = new Pile();
            var card = RndCard();

            pile.Push(card, PilePosition.Middle);

            Assert.AreSame(card, pile.First());
        }

        #endregion Push

        #region Pop

        [Test]
        public void Pop_FromEmptyPile_Throws([Values] PilePosition position)
        {
            var pile = new Pile();

            Assert.Catch<EmptyPileException>(() => pile.Pop(position, 2));
        }

        [Test]
        public void Pop_MoreThanCardsInPile_ReturnsAllCards([Values] PilePosition position)
        {
            var pile = new Pile(new[] { RndCard() });

            var result = pile.Pop(position, 2);

            Assert.AreEqual(0, pile.Size);
            Assert.IsTrue(pile.IsEmpty);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void Pop_CountExactlyAsSizeOfPile_EmptyPile([Values] PilePosition position)
        {
            var pile = new Pile(new[] { RndCard(), RndCard() });

            pile.Pop(position, 2);

            Assert.AreEqual(0, pile.Size);
            Assert.IsTrue(pile.IsEmpty);
        }

        [Test]
        public void Pop_Top_TakesOrderedGroupFromHead()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Top, 2);

            CollectionAssert.IsOrdered(result, new Card.RankValueComparer());
            Assert.AreEqual(2, result.Count, "result size");
            Assert.AreEqual(2, pile.Size);
            Assert.AreSame(source[2], pile.First(), "first");
            Assert.AreSame(source[3], pile.Last(), "last");
        }

        [Test]
        public void Pop_Bottom_TakesOrderedGroupFromTail()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Bottom, 2);

            CollectionAssert.IsOrdered(result, new Card.RankValueComparer());
            Assert.AreEqual(2, result.Count, "result size");
            Assert.AreEqual(2, pile.Size);
            Assert.AreSame(source[0], pile.First(), "first");
            Assert.AreSame(source[1], pile.Last(), "last");
        }

        [Test]
        public void Pop_Middle_TakesOrderedGroup()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Middle, 2);

            CollectionAssert.IsOrdered(result, new Card.RankValueComparer());
            Assert.AreEqual(2, result.Count, "result size");
            Assert.AreEqual(2, pile.Size);
            Assert.AreSame(source[0], pile.First(), "first");
            Assert.AreSame(source[3], pile.Last(), "last");
        }

        [Test]
        public void Pop_Middle_OneLessThanCardsInPile_AlsoTakesCardFromHeadOrTail()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Middle, 3);

            CollectionAssert.IsOrdered(result, new Card.RankValueComparer());
            Assert.AreEqual(3, result.Count, "result size");
            Assert.AreEqual(1, pile.Size);
            Assert.IsTrue(pile.First().Rank.Value == 1 || pile.First().Rank.Value == 4);
        }

        #endregion Pop

        private static Card RndCard(int i = 1)
        {
            return new Card(new Rank("rank" + i, i), new Suit("clubs"));
        }
    }
}