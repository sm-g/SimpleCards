using System.Diagnostics;
using System.Linq;

using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    public interface IFollowSuitInfo
    {
        Suit? LeadingSuit { get; }
        Trump Trump { get; }
        Rank? HighestTrump { get; }
    }

    // https://en.wikipedia.org/wiki/Trick-taking_game#Follow_suit
    public class FollowSuit : SmartEnum<FollowSuit>
    {
        public static readonly FollowSuit None = new FollowSuit(nameof(None), 1);
        public static readonly FollowSuit Regular = new FollowSuit(nameof(Regular), 2);

        /// <summary>
        /// Play-to-beat.
        /// 1) If a player cannot follow suit but can play trump, they must play trump.
        /// 2) If they are able they must beat any trump card already played to the trick. If unable to do so, they must play a trump.
        /// </summary>
        public static readonly FollowSuit MustTrump = new FollowSuit(nameof(MustTrump), 3);

        protected FollowSuit(string name, int value)
            : base(name, value)
        {
        }

        public FollowSuitPlayResult Play(IFollowSuitInfo trickPile, Hand hand, Card card)
        {
            if (Equals(None) ||
                trickPile.LeadingSuit == null ||
                trickPile.LeadingSuit == card.Suit)
            {
                return FollowSuitPlayResult.Played;
            }

            if (Equals(MustTrump))
            {
                var hightestTrumpInPile = trickPile.HighestTrump;
                if (hightestTrumpInPile is not null)
                {
                    if (hand.HasTrump(trickPile.Trump))
                    {
                        var hightestTrumpInHand = hand.CardsInPile
                            .Where(card => trickPile.Trump.IsTrumpCard(card))
                            .OrderByDescending(card => card.Rank)
                            .Select(card => card.Rank)
                            .First();
                        if (hightestTrumpInPile < hightestTrumpInHand && card.Rank != hightestTrumpInHand)
                        {
                            // Player has higher trump card to play
                            return FollowSuitPlayResult.Revoke;
                        }

                        if (!trickPile.Trump.IsTrumpCard(card))
                        {
                            // Player has trump card to play
                            return FollowSuitPlayResult.Revoke;
                        }
                    }
                }

                if (trickPile.Trump.IsTrumpCard(card))
                {
                    return FollowSuitPlayResult.Ruff;
                }
                if (hand.HasSuit(trickPile.LeadingSuit))
                {
                    // Player has card of leading suit to play
                    return FollowSuitPlayResult.Revoke;
                }
            }
            else
            {
                if (hand.HasSuit(trickPile.LeadingSuit))
                {
                    // Player has card of leading suit to play
                    return FollowSuitPlayResult.Revoke;
                }
            }

            Debug.Assert(Equals(Regular), "Not Regular FollowSuit");

            if (trickPile.Trump.IsTrumpCard(card))
            {
                return FollowSuitPlayResult.Ruff;
            }
            return FollowSuitPlayResult.Slough;
        }
    }
}