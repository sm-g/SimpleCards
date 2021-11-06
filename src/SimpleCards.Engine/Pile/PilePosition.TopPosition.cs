using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public abstract partial class PilePosition
    {
        private sealed class TopPosition : PilePosition
        {
            public TopPosition(string name, int value)
                : base(name, value)
            {
            }

            public override Card Peek(Pile pile)
            {
                return pile.First();
            }

            public override List<Card> Pop(Pile pile, int count)
            {
                var result = pile.CardsInPile.Take(count).ToList();
                pile.CardsInPile.RemoveRange(0, result.Count);
                return result;
            }

            public override void Push(Pile pile, Card card)
            {
                pile.CardsInPile.Insert(0, card);
            }

            public override void Push(Pile pile, IReadOnlyCollection<Card> cards)
            {
                pile.CardsInPile.InsertRange(0, cards);
            }
        }
    }
}