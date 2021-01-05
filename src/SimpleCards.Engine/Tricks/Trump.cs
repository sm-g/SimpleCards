using System;
using System.Collections.Generic;

using ByValue;

namespace SimpleCards.Engine
{
    // https://en.wikipedia.org/wiki/Trick-taking_game#Trumps
    // 1) Trump suit could be changed each turn
    // 2) Trump could be concrete Card
    // 3) could be more than one trump suits at the moment
    public sealed class Trump : ValueObject, IComparer<Card>
    {
        private readonly Suit? _trumpSuit;

        private Trump(Suit? suit)
        {
            _trumpSuit = suit;
        }

        public static Trump NoTrump() => new Trump(null);

        public static Trump Static(Suit suit) => new Trump(suit);

        public static Trump Static(Card card) => throw new NotImplementedException("Trump could be concrete Card");

        /// <summary>
        /// Trump defined by stock.
        /// </summary>
        public static Trump LastCard(Pile pile)
        {
            if (pile.IsEmpty)
            {
                throw new ArgumentException("Can not peek card from empty pile");
            }

            return new Trump(pile.Peek(PilePosition.Bottom).Suit);
        }

        /// <summary>
        /// Randomly selected trump.
        /// </summary>
        public static Trump Random(SuitSet suitSet)
        {
            return new Trump(suitSet[Engine.Random.Next(0, suitSet.Count)]);
        }

        public bool IsTrumpCard(Card card)
        {
            return card.Suit == _trumpSuit;
        }

        public int Compare(Card? x, Card? y)
        {
            if (x is null && y is null)
                return 0;
            if (y is null)
                return 1;
            if (x is null)
                return -1;

            if (x.Suit == y.Suit)
                return x.Rank.CompareTo(y.Rank);

            if (IsTrumpCard(x))
                return 1;

            if (IsTrumpCard(y))
                return -1;

            return 0;
        }

        protected override IEnumerable<object> Reflect()
        {
            return new object[]
            {
                _trumpSuit?.Name!,
                _trumpSuit?.Color!
            };
        }
    }
}