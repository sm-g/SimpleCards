using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Game
    {
        private readonly List<AI> _ais = new List<AI>();

        public Game(RankSet ranks, SuitSet suits, Rules rules, Parties parties)
        {
            RankSet = ranks ?? throw new ArgumentNullException(nameof(ranks));
            SuitSet = suits ?? throw new ArgumentNullException(nameof(suits));
            Rules = rules ?? throw new ArgumentNullException(nameof(rules));

            Table = new Table(Rules.ZoneFactory);
            Parties = parties ?? throw new ArgumentNullException(nameof(parties));
            RoundManager = new RoundManager(Table, rules, parties);

            EnsurePartiesValid(parties);
        }

        public RankSet RankSet { get; }
        public SuitSet SuitSet { get; }
        public Rules Rules { get; }

        public Table Table { get; }
        public Parties Parties { get; }
        public RoundManager RoundManager { get; }

        public void Init()
        {
            var pack = Rules.MaterializeRequiredPack(SuitSet, RankSet);

            Table.GameField.PlacePile(pack);

            foreach (var item in Parties)
            {
                _ais.AddRange(item.Players.Select(x => new AI() { Player = x }));
            }
        }

        public void Move(Movement movement)
        {
            var player = Parties.Players.FirstOrDefault(x => x.Name == movement.PlayerName);
            if (player == null)
                throw new ArgumentException($"There is no player {movement.PlayerName} in game");

            if (movement.Action == Action.PlayCard)
            {
                var selectedCard = player.Hand.GetCard(movement.Card!);
                Table.GameField.Pile.Push(selectedCard, PilePosition.Top);
            }
            else
            {
                throw new ArgumentException("Unknown action type: " + movement.Action);
            }

            RoundManager.OnMove(movement);
        }

        private void EnsurePartiesValid(Parties parties)
        {
            var maxPlayers = Rules.GetMaxPlayers(SuitSet, RankSet);
            var playersCount = parties.Players.Count();
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