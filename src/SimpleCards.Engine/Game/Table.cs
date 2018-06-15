using System.Collections.Generic;

namespace SimpleCards.Engine
{
    public class Table
    {
        public Table()
        {
            Zones = new List<Zone>();
        }

        public List<Zone> Zones { get; set; }

        public IReadOnlyCollection<Card> Collect()
        {
            var result = new List<Card>();
            foreach (var z in Zones)
            {
                if (z.Pile.Size > 0)
                    result.AddRange(z.Pile.Pop(PilePosition.Top, z.Pile.Size));
            }
            return result;
        }
    }
}