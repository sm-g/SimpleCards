using System;
using System.Collections;
using System.Collections.Generic;
using Value;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Physical card.
    /// <para/>
    /// Cards are equal (by value) if they have same rank and suit.
    /// <remarks>
    /// <c>card1 == card2</c> will compare abstract cards, i.e. check Queen of Spades
    /// </remarks>
    /// </summary>
    public sealed class Card : ValueType<Card>
    {
        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public Rank Rank { get; }
        public Suit Suit { get; }

        public override string ToString()
        {
            return Rank.Name + " " + Suit.Name;
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] { Rank, Suit };
        }

        public class ByRefComparer : IEqualityComparer<Card>
        {
            public bool Equals(Card x, Card y)
            {
                return ReferenceEquals(x, y);
            }

            public int GetHashCode(Card obj)
            {
                return obj.GetHashCode();
            }
        }

        public class RankValueComparer : IComparer<Card>, IComparer
        {
            public int Compare(Card x, Card y)
            {
                if (x.Rank.Value > y.Rank.Value)
                    return 1;
                if (x.Rank.Value == y.Rank.Value)
                    return 0;
                return -1;
            }

            public int Compare(object x, object y)
            {
                if (!(x is Card f) || !(y is Card s))
                    return 0;
                return Compare(f, s);
            }
        }
    }
}