using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public abstract partial class PilePosition
    {
        private sealed class MiddlePosition : PilePosition
        {
            public MiddlePosition(string name, int value)
                : base(name, value)
            {
            }

            public override Card Peek(Pile pile)
            {
                var i = Random.Next(0, pile.Size);
                return pile.CardsInPile[i];
            }

            public override List<Card> Pop(Pile pile, int count)
            {
                // group of cards from middle
                var seed = Peek(pile);
                var index = pile.CardsInPile.IndexOf(seed);
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

                var result = pile.CardsInPile.Skip(start).Take(count).ToList();
                pile.CardsInPile.RemoveRange(start, result.Count);
                return result;
            }

            public override void Push(Pile pile, Card card)
            {
                if (pile.IsEmpty)
                {
                    pile.CardsInPile.Add(card);
                }
                else
                {
                    var i = Random.Next(1, pile.Size);
                    pile.CardsInPile.Insert(i, card);
                }
            }

            public override void Push(Pile pile, IReadOnlyCollection<Card> cards)
            {
                if (pile.IsEmpty)
                {
                    pile.CardsInPile.AddRange(cards);
                }
                else
                {
                    var i = Random.Next(1, pile.Size);
                    pile.CardsInPile.InsertRange(i, cards);
                }
            }
        }
    }
}