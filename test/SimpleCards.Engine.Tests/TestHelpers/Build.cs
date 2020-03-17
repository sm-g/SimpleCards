using System.Collections.Generic;

namespace SimpleCards.Engine
{
    internal static class Build
    {
        public static Parties Parties(int playersCount) => Parties(playersCount, 1);

        public static Parties Parties(int count, int playersPerPartyCount)
        {
            var parties = new List<Party>();
            var totalPlayers = count * playersPerPartyCount;
            Party party = null;

            // player names must be unique
            for (var playerNumber = 1; playerNumber < totalPlayers + 1; playerNumber++)
            {
                var player = new Player("player" + playerNumber);
                party ??= new Party("friends of " + player.Name);
                party.Players.Add(player);

                if (playerNumber % playersPerPartyCount == 0)
                {
                    parties.Add(party);
                    party = null;
                }
            }
            return new Parties(parties);
        }

        public static Table Table() => new Table(new ZoneFactory());

        public static Table TableWithoutStock() => new Table(new NoStockZoneFactory());

        private class NoStockZoneFactory : ZoneFactory
        {
            public override List<Zone> CreateZones()
            {
                return new List<Zone>
                {
                    Discard(),
                    GameField()
                };
            }
        }
    }
}