using System.Collections;
using System.Collections.Generic;

namespace SimpleCards.Engine
{
    public class CardByRankValueComparer : IComparer<Card>, IComparer
    {
        public static readonly CardByRankValueComparer Instance = new CardByRankValueComparer();

        public int Compare(Card x, Card y)
        {
            if (x is null && y is null)
                return 0;
            if (y is null)
                return 1;
            if (x is null)
                return -1;

            return x.Rank.CompareTo(y.Rank);
        }

        public int Compare(object x, object y)
        {
            return Compare(x as Card, y as Card);
        }
    }
}