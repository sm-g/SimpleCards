using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    // https://en.wikipedia.org/wiki/Trick-taking_game#Follow_suit
    public class FollowSuitPlayResult : SmartEnum<FollowSuitPlayResult>
    {
        public static readonly FollowSuitPlayResult Played = new FollowSuitPlayResult(nameof(Played), 1);

        /// <summary>
        /// Discard a card of another plain suit when player cannot follow suit.
        /// </summary>
        public static readonly FollowSuitPlayResult Slough = new FollowSuitPlayResult(nameof(Slough), 2);

        /// <summary>
        /// Trump the trick by playing a trump card when player cannot follow suit.
        /// </summary>
        public static readonly FollowSuitPlayResult Ruff = new FollowSuitPlayResult(nameof(Ruff), 3);

        /// <summary>
        /// Violation of the follow suit rules.
        /// see https://en.wikipedia.org/wiki/Revoke.
        /// </summary>
        public static readonly FollowSuitPlayResult Revoke = new FollowSuitPlayResult(nameof(Revoke), 4);

        protected FollowSuitPlayResult(string name, int value)
            : base(name, value)
        {
        }
    }
}