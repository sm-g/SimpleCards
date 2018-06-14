using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Optional;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Set of suits used in game.
    /// </summary>
    public class SuitSet : IReadOnlyList<Suit>
    {
        private readonly List<Suit> _suits = new List<Suit>();

        /// <summary>
        /// Creates suit set from list of suits.
        /// </summary>
        public SuitSet(IEnumerable<Suit> suits)
        {
            _suits = new List<Suit>(suits);
        }

        private SuitSet()
        {
        }

        public int Count
        {
            get { return _suits.Count; }
        }

        public Suit this[int index]
        {
            get { return _suits[index]; }
        }

        public Suit this[string name]
        {
            get { return _suits.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>
        /// Creates suit set from enum.
        /// </summary>
        public static SuitSet From<T>(Func<T, int> colorOf) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("not enum");
            }

            var result = new SuitSet();
            foreach (T suit in Enum.GetValues(typeof(T)))
            {
                result._suits.Add(new Suit(suit.ToString(), colorOf(suit)));
            }
            return result;
        }

        public Option<Suit> GetSuit(string suitName)
        {
            var result = _suits.Find(suit => suit.Name == suitName);
            if (result.Equals(default(Suit)))
                return Option.None<Suit>();

            return result.Some();
        }

        public IEnumerator<Suit> GetEnumerator()
        {
            return _suits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _suits.GetEnumerator();
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            // all unique
            Contract.Invariant(_suits.GroupBy(x => x).Count() == _suits.Count);
        }
    }
}