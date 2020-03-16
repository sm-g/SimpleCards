using System;
using System.Collections.Generic;
using System.Linq;

using MoreLinq;

namespace SimpleCards.Engine
{
    public class RoundManager
    {
        private readonly Table _table;
        private readonly Rules _rules;
        private readonly IReadOnlyList<Player> _players;
        private readonly List<Movement> _movements;
        private Player? _eldestPlayer;
        private Player? _defendingPlayer;
        private int _currentPlayerIndex;
        private bool _inTurn;

        public RoundManager(Table table, Rules rules, Parties parties)
        {
            _table = table;
            _rules = rules;
            _players = parties.Players;
            _movements = new List<Movement>();
        }

        public Rotation Rotation { get; } = Rotation.Clockwise;

        public Player CurrentPlayer => _players[_currentPlayerIndex];

        // GetEldestPlayer logic depends on game:
        // The player sitting one seat after the declarer (one with the highest bid and not the dealer) in normal rotation
        public Player GetEldestPlayer() => _players[0];

        public void BeginRound()
        {
            if (_inTurn)
                throw new InvalidOperationException("Round already started");

            _eldestPlayer = GetEldestPlayer();
            _currentPlayerIndex = _players
                .Index()
                .First(pair => pair.Value == _eldestPlayer)
                .Key;

            _defendingPlayer = _players[GetNextPlayerIndex()];

            _movements.Clear();
            _inTurn = true;
        }

        public void OnMove(Movement movement)
        {
            if (!_inTurn)
                throw new InvalidOperationException("Round not started yet");

            _movements.Add(movement);

            _currentPlayerIndex = GetNextPlayerIndex();
        }

        public void EndRound()
        {
            if (!_inTurn)
                throw new InvalidOperationException("Round not started yet");

            if (_table.Stock != null)
            {
                var stockPile = _table.Stock.Pile;
                DrawCardsToFillHands(stockPile);
            }

            _inTurn = false;
        }

        private void DrawCardsToFillHands(Pile stockPile)
        {
            _movements
                .Select(movement => movement.PlayerName)
                .Distinct()
                .OrderBy(name => name == _defendingPlayer?.Name ? 1 : 0) // defending is last
                .Select(name => _players.First(p => p.Name == name).Hand)
                .Select(hand => (hand, lack: _rules.HandSize - hand.Size))
                .TakeWhile(_ => stockPile.Size > 0)
                .Select(x => (x.hand, drawnCards: stockPile.Pop(PilePosition.Top, x.lack)))
                .ForEach(x => x.hand.Push(x.drawnCards, PilePosition.Default));
        }

        private int GetNextPlayerIndex()
        {
            // here could be rules about team play (teammates may be skipped)
            // and about players eliminated until game end
            return Rotation.GetNextPlayerIndex(_currentPlayerIndex, _players.Count);
        }
    }
}