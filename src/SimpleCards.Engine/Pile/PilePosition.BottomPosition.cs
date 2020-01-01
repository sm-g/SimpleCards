using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public abstract partial class PilePosition
    {
        private class BottomPosition : PilePosition
        {
            public BottomPosition(string name, int value)
                : base(name, value)
            {
            }

            public override Card Peek(Pile pile)
            {
                return pile.Last();
            }

            public override List<Card> Pop(Pile pile, ushort count)
            {
                var result = pile.CardsInPile.TakeLast(count).ToList();
                pile.CardsInPile.RemoveRange(pile.CardsInPile.Count - result.Count, result.Count);
                return result;
            }

            public override void Push(Pile pile, Card card)
            {
                pile.CardsInPile.Add(card);
            }

            public override void Push(Pile pile, IReadOnlyCollection<Card> cards)
            {
                pile.CardsInPile.AddRange(cards);
            }
        }
    }
}