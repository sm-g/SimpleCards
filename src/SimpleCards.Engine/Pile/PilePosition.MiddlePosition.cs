using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public abstract partial class PilePosition
    {
        private class MiddlePosition : PilePosition
        {
            public MiddlePosition(string name, int value)
                : base(name, value)
            {
            }

            public override Card Peek(Pile pile)
            {
                var i = Random.Next(0, pile.Size);
                return pile.cardsInPile[i];
            }

            public override List<Card> Pop(Pile pile, ushort count)
            {
                // group of cards from middle
                var seed = Peek(pile);
                var index = pile.cardsInPile.IndexOf(seed);
                var start = 0;
                if (index >= count - 1)
                {
                    // take from beginning to seed
                    start = index - count + 1;
                }
                else if (pile.Size - index >= count)
                {
                    // take from seed to ending
                    start = index;
                }
                else
                {
                    // take around seed
                    start = Math.Max(0, pile.Size - count);
                }

                var result = pile.cardsInPile.Skip(start).Take(count).ToList();
                pile.cardsInPile.RemoveRange(start, result.Count);
                return result;
            }

            public override void Push(Pile pile, Card card)
            {
                if (pile.IsEmpty)
                {
                    pile.cardsInPile.Add(card);
                }
                else
                {
                    var i = Random.Next(1, pile.Size);
                    pile.cardsInPile.Insert(i, card);
                }
            }

            public override void Push(Pile pile, IReadOnlyCollection<Card> cards)
            {
                if (pile.IsEmpty)
                {
                    pile.cardsInPile.AddRange(cards);
                }
                else
                {
                    var i = Random.Next(1, pile.Size);
                    pile.cardsInPile.InsertRange(i, cards);
                }
            }
        }
    }
}