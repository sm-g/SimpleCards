namespace SimpleCards.Engine
{
    public class Rules
    {
        public Rules()
        {
            PackSize = 36;
            HandSize = 6;
        }

        /// <summary>
        /// TODO pack generation
        /// </summary>
        public ushort PackSize { get; set; }

        public ushort HandSize { get; set; }

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