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
            return new Pile(Zones.SelectMany(z => z.Pile.PopAll()));
        }
    }
}