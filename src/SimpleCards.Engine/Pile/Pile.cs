using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;

namespace SimpleCards.Engine
{
    /// <summary>
    /// A set of cards placed on a surface so that they partially or completely overlap.
    /// </summary>
    public class Pile : IReadOnlyCollection<Card>
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
            if (cardsInPile.Contains(card, CardByRefEqualityComparer.Instance))
                throw new ArgumentException("Given card instance already in pile");

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
            if (cards.GroupBy(x => x, CardByRefEqualityComparer.Instance).Any(x => x.Count() > 1))
                throw new ArgumentException("Duplicate instances in given cards");

            if (cardsInPile.Intersect(cards, CardByRefEqualityComparer.Instance).Any())
                throw new ArgumentException("One of given cards instance already in pile");

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
            return cardsInPile.First();
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
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (IsEmpty)
                throw new EmptyPileException(this);

            IList<Card> result;
            switch (p)
            {
                case PilePosition.Top:
                    result = cardsInPile.Take(count).ToList();
                    cardsInPile.RemoveRange(0, result.Count);
                    return result;

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

                    result = cardsInPile.Skip(start).Take(count).ToList();
                    cardsInPile.RemoveRange(start, result.Count);
                    return result;

                case PilePosition.Bottom:
                    result = cardsInPile.TakeLast(count).ToList();
                    cardsInPile.RemoveRange(Size - result.Count, result.Count);
                    return result;
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

        internal int GetIndexOf(Card card)
        {
            for (var i = 0; i < cardsInPile.Count; i++)
            {
                if (CardByRefEqualityComparer.Instance.Equals(card, cardsInPile[i]))
                    return i;
            }
            return -1;
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

        #region IReadOnlyCollection

        public int Count
        {
            get { return cardsInPile.Count; }
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return cardsInPile.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return cardsInPile.GetEnumerator();
        }

        #endregion IReadOnlyCollection
    }
}