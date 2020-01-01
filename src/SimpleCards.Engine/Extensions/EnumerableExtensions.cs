using System.Linq;

using MoreLinq;

namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static bool AllUnique<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IEqualityComparer<TKey> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            return source.CountBy(selector, comparer).All(x => x.Value == 1);
        }

        public static bool AllUnique<T, TKey>(this IReadOnlyCollection<T> source, Func<T, TKey> selector, IEqualityComparer<TKey> comparer = null)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            // use faster implementation when double enumeration is not a problem
            return source.GroupBy(selector, comparer).Count() == source.Count;
        }
    }
}