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

        // TODO there is games where some cards not collected from "Remove From Game" pile

        public Pile Collect()
        {
            var result = new Pile();
            foreach (var z in Zones)
            {
                if (z.Pile.Size > 0)
                    result.Push(z.Pile.Pop(PilePosition.Top, z.Pile.Size), PilePosition.Bottom);
            }
            return result;
        }
    }
}