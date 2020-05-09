using System;
using System.Linq;

namespace SimpleCards.Engine
{
    /// <summary>
    /// The cards held by one player (owner). Visible to owner.
    /// <remarks>Ordered by adding to hand, in UI may be sorted any other way.</remarks>
    /// </summary>
    public class Hand : Pile
    {
        public Hand(Player player)
        {
            Player = player;
        }

        public Player Player { get; }

        public bool Contains(Card card)
        {
            return CardsInPile.Contains(card);
        }

        public bool HasSuit(Suit suit) => CardsInPile.Any(card => card.Suit == suit);

        public bool HasTrump(Trump trump) => CardsInPile.Any(card => trump.IsTrumpCard(card));

        public Card GetCard(Card card)
        {
            var firstMatch = CardsInPile.Find(x => x.Equals(card));
            if (firstMatch == null)
                throw new ArgumentException($"There is no card {card} in hand");

            CardsInPile.Remove(firstMatch);
            return firstMatch;
        }
    }
}