using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleCards.Engine
{
    public class Trick : ReadOnlyCollection<Card>
    {
        public Trick(IList<Card> list, Player taker)
            : base(list)
        {
            Taker = taker;
        }

        public Player Taker { get; }

        // assume Plain trick game

        public int Score => Items.Count;
    }
}