using System.Collections.Generic;
using System.Linq;

using MoreLinq;

namespace SimpleCards.Engine
{
    public class RoundManager
    {
        private readonly IReadOnlyList<Player> _players;
        private int _currentPlayerIndex;

        public RoundManager(Parties parties)
        {
            _players = parties.Players;

            var eldest = GetEldestPlayer();
            _currentPlayerIndex = _players
                .Index()
                .First(pair => pair.Value == eldest)
                .Key;
        }

        public Player CurrentPlayer => _players[_currentPlayerIndex];

        // GetEldestPlayer logic depends on game
        public Player GetEldestPlayer() => _players[0];

        public void BeginRound()
        {
            // empty
        }

        public void OnMove()
        {
            _currentPlayerIndex++;
            if (_currentPlayerIndex == _players.Count)
                _currentPlayerIndex = 0;
        }

        public void EndRound()
        {
            // empty
        }
    }
}