using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public abstract partial class PilePosition
    {
        private class TopPosition : PilePosition
        {
            public TopPosition(string name, int value)
                : base(name, value)
            {
            }

            public override Card Peek(Pile pile)
            {
                return pile.First();
            }

            public override List<Card> Pop(Pile pile, ushort count)
            {
                var result = pile.cardsInPile.Take(count).ToList();
                pile.cardsInPile.RemoveRange(0, result.Count);
                return result;
            }

            public override void Push(Pile pile, Card card)
            {
                pile.cardsInPile.Insert(0, card);
            }

            public override void Push(Pile pile, IReadOnlyCollection<Card> cards)
            {
                pile.cardsInPile.InsertRange(0, cards);
            }
        }
    }
}