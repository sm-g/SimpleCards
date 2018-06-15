using System;
using System.Collections.Generic;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Complete set of cards in game.
    /// Popular packs contain 36, 54, 104 cards.
    /// </summary>
    public class Pack : Pile
    {
        /// <summary>
        /// Creates pile with cards of all suits and ranks in set.
        /// </summary>
        /// <param name="shuffle">If true, shuffles pile after creation.</param>
        public Pack(SuitSet suits, RankSet ranks, bool shuffle = false, int decksCount = 1)
        {
            if (suits == null)
                throw new ArgumentNullException(nameof(suits));
            if (ranks == null)
                throw new ArgumentNullException(nameof(ranks));
            if (decksCount < 1)
                throw new ArgumentOutOfRangeException(nameof(decksCount));

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    for (var i = 0; i < decksCount; i++)
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
    }
}