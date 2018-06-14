using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using SimpleCards.Engine.Extensions;

namespace SimpleCards.Engine
{
    /// <summary>
    /// A set of cards placed on a surface so that they partially or completely overlap.
    /// </summary>
    public class Pile : ICollection<Card>, IReadOnlyCollection<Card>
    {
        private static Random rnd = new Random();
        protected List<Card> cardsInPile;

        /// <summary>
        /// Creates pile with given cards.
        /// </summary>
        public Pile(IEnumerable<Card> cards)
        {
            cardsInPile = new List<Card>(cards);
        }

        /// <summary>
        /// Creates empty pile.
        /// </summary>
        public Pile()
        {
            cardsInPile = new List<Card>();
        }

        /// <summary>
        /// Quantity of cards in the pile.
        /// </summary>
        public ushort Size
        {
            get { return (ushort)cardsInPile.Count; }
        }

        public bool IsEmpty
        {
            get { return Size == 0; }
        }

        public void Push(Card card, PilePosition p)
        {
            Contract.Ensures(Contract.OldValue(Size) == Size + 1);

            switch (p)
            {
                case PilePosition.Top:
                    PushTop(card);
                    return;

                case PilePosition.Middle:
                    PushMiddle(card);
                    return;

                case PilePosition.Bottom:
                    PushBottom(card);
                    return;
            }

            throw new NotImplementedException();
        }

        public void Push(IEnumerable<Card> cards, PilePosition p)
        {
            Contract.Ensures(Contract.OldValue(Size) == Size + cards.Count());

            switch (p)
            {
                case PilePosition.Top:
                    cardsInPile.InsertRange(0, cards);
                    return;

                case PilePosition.Middle:
                    cards.ForAll(x => PushMiddle(x)); // TODO push in sameplace
                    return;

                case PilePosition.Bottom:
                    cardsInPile.AddRange(cards);
                    return;
            }
        }

        protected void PushTop(Card card)
        {
            cardsInPile.Insert(0, card);
        }

        protected void PushBottom(Card card)
        {
            cardsInPile.Add(card);
        }

        protected void PushMiddle(Card card)
        {
            if (cardsInPile.Count < 2)
            {
                cardsInPile.Add(card);
            }
            else
            {
                var i = rnd.Next(1, Size);
                cardsInPile.Insert(i, card);
            }
        }

        public Card Peek(PilePosition p)
        {
            Contract.Ensures(Contract.OldValue(Size) == Size);

            if (IsEmpty)
                throw new EmptyPileException(this);

            switch (p)
            {
                case PilePosition.Top:
                    return PeekTop();

                case PilePosition.Middle:
                    return PeekRandomCard();

                case PilePosition.Bottom:
                    return PeekBottom();
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns card at top of the pile.
        /// </summary>
        /// <returns></returns>
        protected Card PeekTop()
        {
            return cardsInPile[0];
        }

        /// <summary>
        /// Returns card at bottom of the pile.
        /// </summary>
        /// <returns></returns>
        protected Card PeekBottom()
        {
            return cardsInPile.Last();
        }

        protected Card PeekRandomCard()
        {
            var i = rnd.Next(Size);
            return cardsInPile[i];
        }

        public Card Pop(PilePosition p)
        {
            Contract.Ensures(Contract.OldValue(Size) - 1 == Size);

            if (IsEmpty)
                throw new EmptyPileException(this);

            switch (p)
            {
                case PilePosition.Top:
                    return PopTop();

                case PilePosition.Middle:
                    return PopRandomCard();

                case PilePosition.Bottom:
                    return PopBottom();
            }

            throw new NotImplementedException();
        }

        public IList<Card> Pop(PilePosition p, ushort count)
        {
            Contract.Requires(count > 0);
            Contract.Ensures(Math.Max(Contract.OldValue(Size) - count, 0) == Size);
            Contract.Ensures(Contract.Result<IList<Card>>().Count <= count);

            if (IsEmpty)
                throw new EmptyPileException(this);

            IList<Card> res;
            switch (p)
            {
                case PilePosition.Top:
                    res = cardsInPile.Take(count).ToList();
                    cardsInPile.RemoveRange(0, res.Count);
                    return res;

                case PilePosition.Middle:
                    // group of cards from middle
                    var seed = PeekRandomCard();
                    var index = cardsInPile.IndexOf(seed);
                    var start = 0;
                    if (index >= count - 1)
                    {
                        // take from beginning to seed
                        start = index - count + 1;
                    }
                    else if (Size - index >= count)
                    {
                        // take from seed to ending
                        start = index;
                    }
                    else
                    {
                        // take around seed
                        start = Math.Max(0, Size - count);
                    }

                    res = cardsInPile.Skip(start).Take(count).ToList();
                    cardsInPile.RemoveRange(start, res.Count);
                    return res;

                case PilePosition.Bottom:
                    res = (cardsInPile as IEnumerable<Card>).Reverse().Take(count).ToList();
                    cardsInPile.RemoveRange(Size - res.Count, res.Count);
                    return res;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns card at top of the pile and removes it from the pile.
        /// </summary>
        /// <returns></returns>
        protected Card PopTop()
        {
            var card = cardsInPile[0];
            cardsInPile.Remove(card);
            return card;
        }

        /// <summary>
        /// Returns card at bottom of the pile and removes it from the pile.
        /// </summary>
        /// <returns></returns>
        protected Card PopBottom()
        {
            var card = cardsInPile.Last();
            cardsInPile.Remove(card);
            return card;
        }

        /// <summary>
        /// Returns random card from the pile and removes it from the pile.
        /// </summary>
        /// <returns></returns>
        protected Card PopRandomCard()
        {
            var card = PeekRandomCard();
            cardsInPile.Remove(card);
            return card;
        }

        public void Shuffle()
        {
            cardsInPile.Shuffle();
        }

        public override string ToString()
        {
            var builder = new StringBuilder($"Pile with {Size} cards, top is {PeekTop()}, some random is {PeekRandomCard()}. All cards: \n");
            foreach (var card in cardsInPile)
            {
                builder.Append(card);
                builder.AppendLine();
            }
            return builder.ToString();
        }

        #region ICollection

        public virtual void Add(Card item)
        {
            PushMiddle(item);
        }

        public virtual void Clear()
        {
            cardsInPile.Clear();
        }

        public bool Contains(Card item)
        {
            return cardsInPile.Contains(item);
        }

        public void CopyTo(Card[] array, int arrayIndex)
        {
            cardsInPile.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return cardsInPile.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(Card item)
        {
            return cardsInPile.Remove(item);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return cardsInPile.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cardsInPile.GetEnumerator();
        }

        #endregion ICollection

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            // all unique objects
            Contract.Invariant(cardsInPile.Distinct(new Card.ByRefComparer()).Count() == cardsInPile.Count);
        }
    }
}