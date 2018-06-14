using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random Random = new Random();

        public static void ForAll<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> source, int chunksize)
        {
            if (chunksize <= 0)
                throw new ArgumentOutOfRangeException(nameof(chunksize));

            var pos = 0;
            while (source.Skip(pos).Any())
            {
                yield return source.Skip(pos).Take(chunksize);
                pos += chunksize;
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> collection, T item)
        {
            return collection.Concat(new[] { item });
        }

        public static IEnumerable<T> Evens<T>(this IEnumerable<T> collection)
        {
            return collection.Where((r, i) => i % 2 == 0);
        }

        public static IEnumerable<T> Odds<T>(this IEnumerable<T> collection)
        {
            return collection.Where((r, i) => i % 2 != 0);
        }

        public static bool AllUnique<T>(this IEnumerable<T> collection)
        {
            return collection.Distinct().Count() == collection.Count();
        }

        public static bool AllUnique<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> selector)
        {
            return collection.Select(x => selector(x)).Distinct().Count() == collection.Count();
        }

        public static bool IsSingle<T>(this IEnumerable<T> collection)
        {
            return !collection.Any() || collection.Distinct().Count() == 1;
        }

        public static bool IsSingle<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> selector)
        {
            return !collection.Any() || collection.Select(x => selector(x)).Distinct().Count() == 1;
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keyExtractor, IEqualityComparer<TKey> comparer = null)
        {
            return source.Distinct(Compare.By(keyExtractor, comparer));
        }

        /// <summary>
        /// True if two lists contain same items.
        /// from http://stackoverflow.com/questions/3669970/compare-two-listt-objects-for-equality-ignoring-order
        /// </summary>
        public static bool ScrambledEquals<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            if (list1 == null)
                throw new ArgumentNullException(nameof(list1));
            if (list2 == null)
                throw new ArgumentNullException(nameof(list2));

            var cnt = new Dictionary<T, int>();
            foreach (var s in list1)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]++;
                }
                else
                {
                    cnt.Add(s, 1);
                }
            }
            foreach (var s in list2)
            {
                if (cnt.ContainsKey(s))
                {
                    cnt[s]--;
                }
                else
                {
                    return false;
                }
            }
            return cnt.Values.All(c => c == 0);
        }

        /// <summary>
        /// from http://stackoverflow.com/questions/1651619/optimal-linq-query-to-get-a-random-sub-collection-shuffle
        /// </summary>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            var buffer = source.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = Random.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            for (var i = list.Count; i > 1; i--)
            {
                var j = Random.Next(i); // 0 <= j <= i-1
                var tmp = list[j];
                list[j] = list[i - 1];
                list[i - 1] = tmp;
            }
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

        public static string ToSeparatedString(this IEnumerable<Guid> guids)
        {
            if (guids == null)
                throw new ArgumentNullException(nameof(guids));

            return $"'{string.Join("','", guids)}'";
        }

        public static string ToSeparatedString<T>(this IEnumerable<T> collection, string separator)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            return string.Join(separator, collection);
        }
    }
}