using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Game
    {
        private readonly List<AI> _ais = new List<AI>();

        public Game(RankSet rs, SuitSet ss, Rules rules, int players)
        {
            RankSet = rs;
            SuitSet = ss;
            Rules = rules;

            Table = new Table();
            Parties = new List<Party>();
            for (var i = 0; i < players; i++)
            {
                var player = new Player("player" + i);
                var party = new Party();
                party.Players.Add(player);
                Parties.Add(party);
            }
        }

        public Game()
        {
        }

        public RankSet RankSet { get; set; }
        public SuitSet SuitSet { get; set; }
        public Rules Rules { get; set; }

        public Table Table { get; set; }
        public List<Party> Parties { get; set; }

        public Pack Pack { get; set; }

        public void StartPlayers()
        {
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