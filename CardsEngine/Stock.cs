using System.Collections.Generic;

namespace SimpleCards.Engine
{
    /// <summary>
    /// A pile of cards, face down, which are left over after setting up the rest of the game.
    /// </summary>
    public class Stock : Pile
    {
        public Stock(IEnumerable<Card> cards)
            : base(cards)
        {

        }

        public bool IsLastVisible { get; set; }
    }
}