using System.Collections.Generic;

namespace SimpleCards.Engine
{
    internal static class PartiesBuilder
    {
        public static Parties CreateParties(int playersCount)
        {
            var parties = new List<Party>();
            for (var i = 1; i < playersCount + 1; i++)
            {
                var player = new Player("player" + i);
                var party = new Party("friends of " + player.Name);
                party.Players.Add(player);
                parties.Add(party);
            }

            return new Parties(parties);
        }
    }
}