namespace SimpleCards.Engine
{
    public abstract class RuleVisitor
    {
    }

    // pile in zone visible for AI
    // card in pile visibility

    internal class TupleRuleVisitor
    {
        /// <summary>
        /// All pairs, triples etc of cards from hand with same rank.
        /// </summary>
        ////public IEnumerable<Card> GetTuples(AI ai)
        ////{
        ////    return ai.Player.Hand
        ////        .GroupBy(x => x.Rank)
        ////        .Where(g => g.Count() > 1)
        ////        .SelectMany(x => x);
        ////}
    }
}