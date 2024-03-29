﻿using System;
using System.Collections.Generic;
using System.Linq;

using AutoFixture;

using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class PileTests
    {
        private readonly Fixture _f;

        public PileTests()
        {
            _f = new Fixture();
            _f.Customizations.Add(new PilePositionBuilder());
        }

        [Test]
        public void Ctor_UsesGivenCardsInGivenOrder()
        {
            var source = new[] { RndCard(3), RndCard(1), RndCard(3), RndCard(4) };

            var result = new Pile(source);

            Assert.That(result, Is.EqualTo(source).Using(CardByRefEqualityComparer.Instance));
        }

        #region Push

        [Test]
        public void Push_AddsCard()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();
            var card = RndCard();

            pile.Push(card, position);

            Assert.AreEqual(1, pile.Size);
        }

        [Test]
        public void Push_AddsAllCards()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();

            pile.Push(new[] { RndCard(), RndCard() }, position);

            Assert.AreEqual(2, pile.Size);
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
        public void Push_Top_AddedCardsAreFirst()
        {
            var pile = new Pile(new[] { RndCard(), RndCard(), RndCard() });
            var cards = new[] { RndCard(), RndCard() };

            pile.Push(cards, PilePosition.Top);

            CollectionAssert.AreEqual(cards, pile.Take(2));
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
        public void Push_Bottom_AddedCardsAreLast()
        {
            var pile = new Pile(new[] { RndCard(), RndCard(), RndCard() });
            var cards = new[] { RndCard(), RndCard() };

            pile.Push(cards, PilePosition.Bottom);

            CollectionAssert.AreEqual(cards, pile.TakeLast(2));
        }

        [Test]
        public void Push_Middle_AddedCardAlwaysInDifferentPlace()
        {
            var addedCard = RndCard();

            var dict = new Dictionary<int, int>();
            for (var i = 0; i < 50; i++)
            {
                var pile = new Pile(Enumerable.Repeat(RndCard(), 4));

                pile.Push(addedCard, PilePosition.Middle);

                IncValueForKey(dict, pile.GetIndexOf(addedCard));
            }
            Assert.That(dict.Select(x => x.Value), Has.All.GreaterThan(1));
            Assert.IsFalse(dict.ContainsKey(0), "not first");
            Assert.IsFalse(dict.ContainsKey(4), "not last");
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
        public void Push_Middle_AddedCardsAllInRandomPlacesButNotFirstOrLast()
        {
            var dict = new Dictionary<int, int>();
            for (var i = 0; i < 50; i++)
            {
                var pile = new Pile(new[] { RndCard(), RndCard(), RndCard() });
                var cards = new[] { RndCard(), RndCard() };

                pile.Push(cards, PilePosition.Middle);

                IncValueForKey(dict, pile.GetIndexOf(cards[0]));
                IncValueForKey(dict, pile.GetIndexOf(cards[1]));
            }
            Assert.That(dict.Select(x => x.Value), Has.All.GreaterThan(1));
            Assert.IsFalse(dict.ContainsKey(0), "not first");
            Assert.IsFalse(dict.ContainsKey(4), "not last");
        }

        [Test]
        public void Push_EmptyPile_Ok()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();
            var card = RndCard();

            pile.Push(card, position);

            Assert.AreSame(card, pile.First());
        }

        [Test]
        public void Push_CardInstanceAlreadyInPile_Throws()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();
            var card = RndCard();
            pile.Push(card, position);

            var ex = Assert.Catch<ArgumentException>(() => pile.Push(card, position));
            Assert.That(ex!.Message, Contains.Substring("instance already in pile").IgnoreCase);
        }

        [Test]
        public void Push_DuplicateOfCardInstance_Throws()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();
            var card = RndCard();

            var ex = Assert.Catch<ArgumentException>(() => pile.Push(new[] { card, card }, position));
            Assert.That(ex!.Message, Contains.Substring("Duplicate instances").IgnoreCase);
        }

        [Test]
        public void Push_SetWithCardInstanceAlreadyInPile_Throws()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();
            var card = RndCard();
            pile.Push(card, position);
            pile.Push(RndCard(), position);

            var ex = Assert.Catch<ArgumentException>(() => pile.Push(new[] { RndCard(), card }, position));
            Assert.That(ex!.Message, Contains.Substring("instance already in pile").IgnoreCase);
        }

        #endregion Push

        #region Peek

        [Test]
        public void Peek_FromEmptyPile_Throws()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();

            var ex = Assert.Catch<InvalidOperationException>(() => pile.Peek(position));
            Assert.That(ex!.Message, Contains.Substring("is empty").IgnoreCase);
        }

        [Test]
        public void Peek_Top_ReturnsFirstCard()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Peek(PilePosition.Top);

            Assert.AreSame(source[0], result);
            Assert.AreEqual(source.Count(), pile.Size);
        }

        [Test]
        public void Peek_Bottom_ReturnsLasstCard()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Peek(PilePosition.Bottom);

            Assert.AreSame(source[3], result);
            Assert.AreEqual(source.Count(), pile.Size);
        }

        [Test]
        public void Peek_Middle_ReturnsRandomCard()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };

            var dict = new Dictionary<int, int>();
            for (var i = 0; i < 50; i++)
            {
                var pile = new Pile(source);

                var result = pile.Peek(PilePosition.Middle);

                IncValueForKey(dict, result.Rank.Value);
                Assert.AreEqual(source.Count(), pile.Size);
            }
            Assert.That(dict.Select(x => x.Value), Has.All.GreaterThan(1));
            Assert.AreEqual(4, dict.Keys.Count);
        }

        #endregion Peek

        #region Pop

        [Test]
        public void Pop_FromEmptyPile_Throws()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();

            var ex = Assert.Catch<InvalidOperationException>(() => pile.Pop(position));
            Assert.That(ex!.Message, Contains.Substring("is empty").IgnoreCase);
        }

        [Test]
        public void Pop_ManyFromEmptyPile_Throws()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile();

            var ex = Assert.Catch<InvalidOperationException>(() => pile.Pop(position, 2));
            Assert.That(ex!.Message, Contains.Substring("is empty").IgnoreCase);
        }

        [Test]
        public void Pop_MoreThanCardsInPile_ReturnsAllCards()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile(new[] { RndCard() });

            var result = pile.Pop(position, 2);

            Assert.AreEqual(0, pile.Size);
            Assert.IsTrue(pile.IsEmpty);
            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void Pop_CountExactlyAsSizeOfPile_EmptyPile()
        {
            var position = _f.Create<PilePosition>();
            var pile = new Pile(new[] { RndCard(), RndCard() });

            pile.Pop(position, 2);

            Assert.AreEqual(0, pile.Size);
            Assert.IsTrue(pile.IsEmpty);
        }

        [Test]
        public void Pop_Top_TakesFromHead()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Top);

            Assert.AreEqual(3, pile.Size);
            Assert.AreSame(source[0], result);
        }

        [Test]
        public void Pop_Top_TakesOrderedGroupFromHead()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Top, 2);

            CollectionAssert.AreEqual(new[] { source[0], source[1] }, result);
            CollectionAssert.AreEqual(new[] { source[2], source[3] }, pile);
        }

        [Test]
        public void Pop_Bottom_TakesFromTail()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Bottom);

            Assert.AreEqual(3, pile.Size);
            Assert.AreSame(source[3], result);
        }

        [Test]
        public void Pop_Bottom_TakesOrderedGroupFromTail()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Bottom, 2);

            CollectionAssert.AreEqual(new[] { source[2], source[3] }, result);
            CollectionAssert.AreEqual(new[] { source[0], source[1] }, pile);
        }

        [Test]
        public void Pop_Middle_TakesRandomCard()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };

            var dict = new Dictionary<int, int>();
            for (var i = 0; i < 50; i++)
            {
                var pile = new Pile(source);

                var result = pile.Pop(PilePosition.Middle);

                IncValueForKey(dict, result.Rank.Value);
            }
            Assert.That(dict.Select(x => x.Value), Has.All.GreaterThan(1));
            Assert.AreEqual(4, dict.Keys.Count);
        }

        [Test]
        public void Pop_Middle_TakesOrderedGroup()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            using (Random.Returing(new[] { 2 }))
            {
                var result = pile.Pop(PilePosition.Middle, 2);

                CollectionAssert.AreEqual(new[] { source[1], source[2] }, result);
                Assert.AreEqual(2, result.Count, "result size");
                Assert.AreEqual(2, pile.Size);
                Assert.AreSame(source[0], pile.First(), "first");
                Assert.AreSame(source[3], pile.Last(), "last");
            }
        }

        [Test]
        public void Pop_Middle_OneLessThanCardsInPile_AlsoTakesCardFromHeadOrTail()
        {
            var source = new[] { RndCard(1), RndCard(2), RndCard(3), RndCard(4) };
            var pile = new Pile(source);

            var result = pile.Pop(PilePosition.Middle, 3);

            Assert.AreEqual(3, result.Count, "result size");
            Assert.AreEqual(1, pile.Size);
            Assert.IsTrue(pile.First().Rank.Value == 1 || pile.First().Rank.Value == 4);
        }

        #endregion Pop

        private static Card RndCard(int rank = 1) => Stub.Cards.Rnd(rank);

        private static void IncValueForKey(Dictionary<int, int> dict, int key)
        {
            if (dict.ContainsKey(key))
                dict[key]++;
            else
                dict[key] = 1;
        }
    }
}