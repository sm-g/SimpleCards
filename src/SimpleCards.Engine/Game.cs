using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCards.Engine
{
    public class Game
    {
        public Game(RankSet rs, SuitSet ss, Rules rules, int players)
        {
            RankSet = rs;
            SuitSet = ss;
            Rules = rules;

            Table = new Table();
            Parties = new List<Party>();
            for (int i = 0; i < players; i++)
            {
                var player = new Player("player" + i);
                var party = new Party();
                party.Players.Add(player);
                Parties.Add(party);
            }
        }

        public Game() { }
        public RankSet RankSet { get; set; }
        public SuitSet SuitSet { get; set; }
        public Rules Rules { get; set; }

        public Table Table { get; set; }
        public List<Party> Parties { get; set; }

        public Pack Pack { get; set; }

        List<AI> ais = new List<AI>();
        public void StartPlayers()
        {
            foreach (var item in Parties)
            {
                ais.AddRange(item.Players.Select(x => new AI() { Player = x }));
            }
        }

        public void Move()
        {

        }
    }
}
