using System.Collections.Generic;

namespace SimpleCards.Engine
{
    public class CardByRefEqualityComparer : IEqualityComparer<Card>
    {
        public static readonly CardByRefEqualityComparer Instance = new CardByRefEqualityComparer();

        public bool Equals(Card x, Card y)
        {
            return ReferenceEquals(x, y);
        }

        public int GetHashCode(Card obj)
        {
            return obj.GetHashCode();
        }
    }
}