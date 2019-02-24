using System;
using System.Collections.Generic;

namespace SimpleCards.Engine
{
    public static class Random
    {
        private static readonly System.Random DefaultGenerator = new System.Random();
        private static readonly Func<int, int, int> DefaultNext = DefaultGenerator.Next;

        /// <summary>
        /// Returns a random integer that is within a specified range (lower bound inclusive, upper bound exclusive).
        /// </summary>
        public static Func<int, int, int> Next { get; private set; } = DefaultNext;

        public static IDisposable Returing(IReadOnlyCollection<int> values)
        {
            if (values.Count == 0)
                throw new ArgumentException("At least one value required");

            var enumerator = values.GetEnumerator();
            Next = (min, max) =>
            {
                if (!enumerator.MoveNext())
                {
                    enumerator.Reset();
                    enumerator.MoveNext();
                }
                return enumerator.Current;
            };
            return new Impl();
        }

#pragma warning disable S3881 // "IDisposable" should be implemented correctly

        private class Impl : IDisposable
        {
            public void Dispose()
            {
                Next = DefaultNext;
            }
        }

#pragma warning restore S3881 // "IDisposable" should be implemented correctly
    }
}