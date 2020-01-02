using System.Collections.Generic;

namespace SimpleCards.Engine
{
    public class ZoneFactory
    {
        /// <summary>
        /// Creates <see cref="Zone"/>s specific for some concrete game.
        /// </summary>
        public List<Zone> CreateZones()
        {
            return new List<Zone>
            {
                Discard(),
                GameField(),
                Stock()
            };
        }

        public Zone Discard()
        {
            return new Zone(Zone.DiscardName);
        }

        public Zone GameField()
        {
            return new Zone(Zone.GameFieldName);
        }

        public Zone Stock()
        {
            return new Zone(Zone.StockName);
        }
    }
}