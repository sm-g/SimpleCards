using System;
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
    public class Card : ValueType<Card>
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
    }
}