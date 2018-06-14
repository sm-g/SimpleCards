using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine.Extensions
{
    public class KeyEqualityComparer<T, TKey> : IEqualityComparer<T>
    {
        protected readonly Func<T, TKey> KeyExtractor;
        protected readonly IEqualityComparer<TKey> Comparer;

        public KeyEqualityComparer(Func<T, TKey> keyExtractor, IEqualityComparer<TKey> comparer = null)
        {
            this.KeyExtractor = keyExtractor;
            this.Comparer = comparer ?? EqualityComparer<TKey>.Default;
        }

        public virtual bool Equals(T x, T y)
        {
            return Comparer.Equals(this.KeyExtractor(x), this.KeyExtractor(y));
        }

        public int GetHashCode(T obj)
        {
            return Comparer.GetHashCode(this.KeyExtractor(obj));
        }
    }

    public class StrictKeyEqualityComparer<T, TKey> : KeyEqualityComparer<T, TKey>
        where TKey : IEquatable<TKey>
    {
        public StrictKeyEqualityComparer(Func<T, TKey> keyExtractor)
            : base(keyExtractor)
        { }

        public override bool Equals(T x, T y)
        {
            // This will use the overload that accepts a TKey parameter
            // instead of an object parameter.
            return this.KeyExtractor(x).Equals(this.KeyExtractor(y));
        }
    }

    internal static class Compare
    {
        public static IEqualityComparer<TSource> By<TSource, TKey>(Func<TSource, TKey> identitySelector, IEqualityComparer<TKey> comparer = null)
        {
            return new KeyEqualityComparer<TSource, TKey>(identitySelector, comparer);
        }
    }
}