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
                result.Push(z.Pile.PopAll(), PilePosition.Bottom);
            }
            return result;
        }
    }
}