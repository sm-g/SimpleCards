using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Table
    {
        public Table(ZoneFactory zoneFactory)
        {
            Zones = zoneFactory.CreateZones();
        }

        public IReadOnlyCollection<Zone> Zones { get; }

        public Zone? Discard => Zones.FirstOrDefault(z => z.Name == Zone.DiscardName);
        public Zone GameField => Zones.First(z => z.Name == Zone.GameFieldName);
        public Zone? Stock => Zones.FirstOrDefault(z => z.Name == Zone.StockName);

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