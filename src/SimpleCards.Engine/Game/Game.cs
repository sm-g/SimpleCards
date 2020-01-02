using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Game
    {
        private readonly List<AI> _ais = new List<AI>();

        public Game(RankSet ranks, SuitSet suits, Rules rules, IReadOnlyList<Party> parties)
        {
            RankSet = ranks ?? throw new ArgumentNullException(nameof(ranks));
            SuitSet = suits ?? throw new ArgumentNullException(nameof(suits));
            Rules = rules ?? throw new ArgumentNullException(nameof(rules));

            Table = new Table(Rules.ZoneFactory);
            Parties = parties ?? throw new ArgumentNullException(nameof(parties));

            EnsurePartiesValid(parties);
        }

        public RankSet RankSet { get; }
        public SuitSet SuitSet { get; }
        public Rules Rules { get; }

        public Table Table { get; }
        public IReadOnlyList<Party> Parties { get; }

        public void Init()
        {
            var pack = Rules.MaterializeRequiredPack(SuitSet, RankSet);

            Table.GameField.Pile.Push(pack, PilePosition.Default);

            foreach (var item in Parties)
            {
                _ais.AddRange(item.Players.Select(x => new AI() { Player = x }));
            }
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        private void EnsurePartiesValid(IReadOnlyList<Party> parties)
        {
            if (!parties.AllUnique(x => x.Name))
                throw new ArgumentException("There are parties with same names", nameof(parties));

            var playersWithParties = from party in parties
                                     from player in party.Players
                                     select (party, player);
            if (!playersWithParties.AllUnique(x => x.player))
                throw new ArgumentException("There is player in many parties at same time", nameof(parties));

            var maxPlayers = Rules.GetMaxPlayers(SuitSet, RankSet);
            var playersCount = playersWithParties.Count();
            if (playersCount > maxPlayers)
            {
                var ex = new ArgumentException("Too many players", nameof(parties));
                ex.Data.Add(nameof(playersCount), playersCount);
                ex.Data.Add(nameof(maxPlayers), maxPlayers);
                throw ex;
            }
        }
    }
}