using System.Collections.Generic;

namespace SimpleCards.Engine
{
    /// <summary>
    /// The cards held by one player (owner). Visible to owner.
    /// <remarks>Ordered by adding to hand, in UI may be sorted any other way.</remarks>
    /// </summary>
    public class Hand : Pile
    {
        public Hand(Player p)
        {
            Player = p;
        }

        public Player Player { get; }

        // TODO we should be able to take concrete card from hand

        public int IndexOf(Card item)
        {
            return cardsInPile.IndexOf(item);
        }

        public void Insert(int index, Card item)
        {
            cardsInPile.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            cardsInPile.RemoveAt(index);
        }

        public bool Contains(Card item)
        {
            return cardsInPile.Contains(item);
        }

        public Card this[int index]
        {
            get
            {
                return cardsInPile[index];
            }
            set
            {
                cardsInPile[index] = value;
            }
        }
    }
}