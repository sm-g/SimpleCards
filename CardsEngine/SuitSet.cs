using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Functional.Maybe;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Set of suits used in game.
    /// </summary>
    public class SuitSet : IReadOnlyList<Suit>
    {
        private List<Suit> suits = new List<Suit>();

        /// <summary>
        /// Crestes suit set from list of suits.
        /// </summary>
        /// <param name="suits"></param>
        public SuitSet(IEnumerable<Suit> suits)
        {
            this.suits = new List<Suit>(suits);
        }

        private SuitSet()
        {
        }

        public int Count
        {
            get { return suits.Count; }
        }

        public Suit this[int index]
        {
            get { return suits[index]; }
        }

        public Suit this[string name]
        {
            get { return suits.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
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
                result.suits.Add(new Suit(suit.ToString(), colorOf(suit)));
            }
            return result;
        }

        public Maybe<Suit> GetSuit(string suitName)
        {
            var result = suits.Find(suit => suit.Name == suitName);
            if (result.Equals(default(Suit)))
                return Maybe<Suit>.Nothing;

            return result.ToMaybe();
        }

        public IEnumerator<Suit> GetEnumerator()
        {
            return suits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return suits.GetEnumerator();
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            // all unique
            Contract.Invariant(suits.GroupBy(x => x).Count() == suits.Count);
        }
    }
}