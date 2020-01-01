namespace SimpleCards.Engine
{
    public class Rules
    {
        public int DecksCount { get; set; } = 1;

        public int HandSize { get; set; } = 6;

        public ZoneFactory ZoneFactory { get; } = new ZoneFactory();

        public Dealer Dealer { get; } = new Dealer();

        public Pack MaterializeRequiredPack(SuitSet suits, RankSet ranks) => new Pack(suits, ranks, true, DecksCount);

        /// <summary>
        /// Gets cost of card.
        /// </summary>
        /// <returns></returns>
        public int ValueOf(Rank r, Suit s)
        {
            return r.Value;
        }
    }
}