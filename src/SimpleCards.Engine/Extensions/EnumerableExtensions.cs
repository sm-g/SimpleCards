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

        /// <summary>
        /// True if items ordered by not descending key (next >= prev).
        /// </summary>
        public static bool IsOrdered<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keyExtractor)
            where TKey : IComparable<TKey>
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (keyExtractor == null)
                throw new ArgumentNullException(nameof(keyExtractor));

            return items.Zip(items.Skip(1), (a, b) => new { a, b })
                .All(x => (keyExtractor(x.a).CompareTo(keyExtractor(x.b)) <= 0));
        }

        /// <summary>
        /// True if items ordered by ascending key without equal keys (next > prev).
        /// </summary>
        public static bool IsStrongOrdered<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keyExtractor)
            where TKey : IComparable<TKey>
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (keyExtractor == null)
                throw new ArgumentNullException(nameof(keyExtractor));

            return items.Zip(items.Skip(1), (a, b) => new { a, b })
                .All(x => keyExtractor(x.a).CompareTo(keyExtractor(x.b)) < 0);
        }
    }
}