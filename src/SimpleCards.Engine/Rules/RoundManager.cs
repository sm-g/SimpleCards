using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class RoundManager
    {
        private readonly Parties _parties;
        private readonly List<Player> _players;
        private int _currentPlayerIndex;

        public RoundManager(Parties parties)
        {
            _parties = parties;
            _players = parties.Players.ToList();

            var eldest = GetEldestPlayer();
            _currentPlayerIndex = _players.IndexOf(eldest);
        }

        public Player CurrentPlayer => _players[_currentPlayerIndex];

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