namespace SimpleCards.Engine
{
    public class Rules
    {
        public int DecksCount { get; set; } = 1;

        // TODO HandSize depends on players count
        public int HandSize { get; set; } = 6;

        public ZoneFactory ZoneFactory { get; } = new ZoneFactory();

        public Pack MaterializeRequiredPack(SuitSet suits, RankSet ranks) => new Pack(suits, ranks, true, DecksCount);

        public int GetMaxPlayers(SuitSet suits, RankSet ranks)
        {
            var pack = MaterializeRequiredPack(suits, ranks);
            return pack.Size / HandSize;
        }

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