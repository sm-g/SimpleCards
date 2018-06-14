using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Complete set of cards in game.
    /// Popular packs contain 36, 54, 104 cards.
    /// </summary>
    public class Pack : Pile
    {
        /// <summary>
        /// Creates pile with cards of all suits and ranks in set, grouped by suits.
        /// </summary>
        /// <param name="suits"></param>
        /// <param name="ranks"></param>
        /// <param name="shuffle">If true, shuffles pile after creation.</param>
        public Pack(SuitSet suits, RankSet ranks, bool shuffle = true, int number = 1)
        {
            cardsInPile = new List<Card>();
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    for (int i = 0; i < number; i++)
                    {
                        cardsInPile.Add(new Card(rank, suit));
                    }
                }
            }
            if (shuffle)
            {
                Shuffle();
            }
        }

        #region ICollection

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override void Clear()
        {
            throw new InvalidOperationException();
        }

        public override void Add(Card item)
        {
            throw new InvalidOperationException();
        }

        public override bool Remove(Card item)
        {
            throw new InvalidOperationException();
        }

        #endregion ICollection
    }
}