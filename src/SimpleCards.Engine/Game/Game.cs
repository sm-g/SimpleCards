using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Game
    {
        private readonly List<AI> _ais = new List<AI>();

        public Game(RankSet ranks, SuitSet suits, Rules rules, int players)
        {
            if (players < 1)
                throw new ArgumentOutOfRangeException(nameof(players));

            RankSet = ranks ?? throw new ArgumentNullException(nameof(ranks));
            SuitSet = suits ?? throw new ArgumentNullException(nameof(suits));
            Rules = rules ?? throw new ArgumentNullException(nameof(rules));

            Table = new Table(Rules.ZoneFactory);
            var parties = new List<Party>();
            for (var i = 1; i < players + 1; i++)
            {
                var player = new Player("player" + i);
                var party = new Party("friends of " + player.Name);
                party.Players.Add(player);
                parties.Add(party);
            }

            Parties = parties;
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
    }
}