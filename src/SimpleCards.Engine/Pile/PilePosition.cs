using System.Collections.Generic;
using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    public abstract partial class PilePosition : SmartEnum<PilePosition>
    {
        public static readonly PilePosition Top = new TopPosition(nameof(Top), 1);
        public static readonly PilePosition Middle = new MiddlePosition(nameof(Middle), 2);
        public static readonly PilePosition Bottom = new BottomPosition(nameof(Bottom), 3);

        protected PilePosition(string name, int value)
            : base(name, value)
        {
        }

        public abstract Card Peek(Pile pile);

        /// <summary>
        /// Returns card at current position of the pile and removes it from the pile.
        /// </summary>
        public Card Pop(Pile pile)
        {
            var card = Peek(pile);
            pile.cardsInPile.Remove(card);
            return card;
        }

        public abstract List<Card> Pop(Pile pile, ushort count);

        public abstract void Push(Pile pile, Card card);

        public abstract void Push(Pile pile, IReadOnlyCollection<Card> cards);
    }
}