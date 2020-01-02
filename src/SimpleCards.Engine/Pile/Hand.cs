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
    }
}