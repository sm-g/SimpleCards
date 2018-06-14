using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Physical card.
    /// </summary>
    public sealed class Card : IEquatable<Card>
    {
        public Rank Rank { get; private set; }
        public Suit Suit { get; private set; }

        public Card(Rank rank, Suit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public override string ToString()
        {
            return Rank.Name + " " + Suit.Name;
        }

        /// <summary>
        /// Cards are equals by value if they have same rank and suit.
        ///
        /// Use to compare abscract cards, i.e. check Queen of Spades.
        /// </summary>
        public bool Equals(Card other)
        {
            return Rank.Equals(other.Rank) &&
                   Suit.Equals(other.Suit);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Card;
            if (other == null)
                return false;

            return Equals(other);
        }

        public override int GetHashCode()
        {
#pragma warning disable S3249 // Classes directly extending "object" should not call "base" in "GetHashCode" or "Equals"
            return base.GetHashCode();
#pragma warning restore S3249 // Classes directly extending "object" should not call "base" in "GetHashCode" or "Equals"
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
                var f = x as Card;
                var s = y as Card;
                if (f == null || s == null)
                    return 0;
                return Compare(f, s);
            }
        }
    }
}