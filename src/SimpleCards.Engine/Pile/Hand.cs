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

        public bool Contains(Card item)
        {
            return cardsInPile.Contains(item);
        }
    }
}