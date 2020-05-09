using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    /// <summary>
    ///  Single-decks games supported only.
    /// </summary>
    public class TrickPile : Pile, IFollowSuitInfo
    {
        private readonly Dictionary<Player, Card> _playedCards = new Dictionary<Player, Card>();
        private readonly FollowSuit _followSuit;
        private readonly Trump _trump;

        public TrickPile(FollowSuit followSuit, Trump trump)
        {
            _followSuit = followSuit;
            _trump = trump;
        }

        private Card? FirstCard => IsEmpty
            ? null
            : Peek(PilePosition.Bottom);

        /// <summary>
        /// First player, declaring suit.
        /// </summary>
        protected Player? Leader => _playedCards.FirstOrDefault(pair => FirstCard == pair.Value).Key;

        public Suit? LeadingSuit => FirstCard?.Suit;

        public bool HasTrumpCard => CardsInPile.Any(card => _trump.IsTrumpCard(card));

        public Trump Trump => _trump;

        public Rank? HighestTrump => CardsInPile
            .Where(card => _trump.IsTrumpCard(card))
            .OrderByDescending(card => card.Rank)
            .Select(card => card.Rank)
            .FirstOrDefault();

        public void Push(Card card, Player player)
        {
            var result = _followSuit.Play(this, player.Hand, card);
            if (result == FollowSuitPlayResult.Revoke)
            {
                // TODO handle revoke
            }

            Push(card, PilePosition.Top);

            // each player can push only once
            _playedCards.Add(player, card);
        }

        // TODO override Pile.Push to prevent illegal initialization

        public Trick GetTrick()
        {
            var trumpWasPlayed = _playedCards.Values.Any(card => _trump.IsTrumpCard(card));
            var taker = (from pair in _playedCards
                         let player = pair.Key
                         let card = pair.Value
                         where !trumpWasPlayed || _trump.IsTrumpCard(card)
                         orderby card.Rank descending
                         select player).First();

            return new Trick(CardsInPile, taker);
        }
    }

    public class DefenceTrickPile
    {
        protected Player? Leader { get; set; }
        protected Dictionary<Player, List<Card>> Dict { get; } = new Dictionary<Player, List<Card>>();

        public void Push(Card card, Player player)
        {
            if (Dict.Count == 0)
                Leader = player;

            if (!Dict.TryGetValue(player, out var list))
                Dict[player] = list = new List<Card>();

            list.Add(card);
        }
    }
}