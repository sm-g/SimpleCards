using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace SimpleCards.Engine
{
    public enum PilePosition
    {
        Top,
        Middle,
        Bottom
    }

    /// <summary>
    /// Some stacked cards.
    /// </summary>
    public class Pile : ICollection<Card>, IReadOnlyCollection<Card>
    {
        internal List<Card> cardsInPile;
        private static Random rnd = new Random();

        /// <summary>
        /// Creates pile with given cards.
        /// </summary>
        /// <param name="cards"></param>
        public Pile(IEnumerable<Card> cards)
        {
            cardsInPile = new List<Card>(cards);
        }

        /// <summary>
        /// Creates pile with cards of all suits and ranks in set, grouped by suits.
        /// </summary>
        /// <param name="suits"></param>
        /// <param name="ranks"></param>
        /// <param name="shuffle">If true, shuffles pile after creation.</param>
        public Pile(SuitSet suits, RankSet ranks, bool shuffle = true)
        {
            cardsInPile = new List<Card>();
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    cardsInPile.Add(new Card(rank, suit));
                }
            }
            if (shuffle)
            {
                Shuffle();
            }
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
        public int Size
        {
            get { return cardsInPile.Count; }
        }

        public bool IsEmpty
        {
            get { return Size == 0; }
        }

        public void Push(Card card, PilePosition p)
        {
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

        public void PushTop(Card card)
        {
            cardsInPile.Insert(0, card);
        }

        public void PushBottom(Card card)
        {
            cardsInPile.Add(card);
        }

        public void PushMiddle(Card card)
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
        public Card PeekTop()
        {
            if (IsEmpty)
                throw new EmptyPileException(this);
            return cardsInPile[0];
        }

        /// <summary>
        /// Returns card at bottom of the pile.
        /// </summary>
        /// <returns></returns>
        public Card PeekBottom()
        {
            if (IsEmpty)
                throw new EmptyPileException(this);
            return cardsInPile.Last();
        }

        public Card PeekRandomCard()
        {
            if (IsEmpty)
                throw new EmptyPileException(this);
            var i = rnd.Next(Size);
            return cardsInPile[i];
        }

        public Card Pop(PilePosition p)
        {
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

        /// <summary>
        /// Returns card at top of the pile and removes it from the pile.
        /// </summary>
        /// <returns></returns>
        public Card PopTop()
        {
            var card = PeekTop();
            cardsInPile.Remove(card);
            return card;
        }

        /// <summary>
        /// Returns card at bottom of the pile and removes it from the pile.
        /// </summary>
        /// <returns></returns>
        public Card PopBottom()
        {
            var card = PeekBottom();
            cardsInPile.Remove(card);
            return card;
        }

        /// <summary>
        /// Returns random card from the pile and removes it from the pile.
        /// </summary>
        /// <returns></returns>
        public Card PopRandomCard()
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
            string result = string.Format("Pile with {0} cards, top is {1}, some random is {2}. All cards: \n", Size, PeekTop(), PeekRandomCard());
            foreach (var card in cardsInPile)
            {
                result += card.ToString() + "\n";
            }
            return result;
        }

        #region ICollcetion

        public void Add(Card item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
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

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Card item)
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

        #endregion ICollcetion

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            // all unique objects
            Contract.Invariant(cardsInPile.Distinct(new Card.ByRefComparer()).Count() == cardsInPile.Count);
        }

    }
}