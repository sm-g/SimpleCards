using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Parties : ReadOnlyCollection<Party>
    {
        public Parties(IList<Party> parties)
            : base(parties)
        {
            EnsurePartiesValid(parties);

            // assume Players list of Party will not change (however it _could_ be changed by code, because it is IList)
            Players = Items.SelectMany(party => party.Players).ToList();
        }

        public IReadOnlyList<Player> Players { get; }

        private void EnsurePartiesValid(IList<Party> parties)
        {
            if (!parties.AllUnique(x => x.Name))
                throw new ArgumentException("There are parties with same names", nameof(parties));

            var playersWithParties = from party in parties
                                     from player in party.Players
                                     select (party, player);
            if (!playersWithParties.AllUnique(x => x.player))
                throw new ArgumentException("There is player in many parties at same time", nameof(parties));

            if (!playersWithParties.AllUnique(x => x.player.Name))
                throw new ArgumentException("There are players with same names", nameof(parties));
        }
    }
}