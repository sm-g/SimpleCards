using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Functional.Maybe;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Set of ranks in game.
    /// </summary>
    public class RankSet : IReadOnlyList<Rank>
    {
        List<Rank> ranks = new List<Rank>();

        public RankSet(IEnumerable<Rank> ranks)
        {
            this.ranks = new List<Rank>(ranks);
        }

        private RankSet()
        {
        }

        public Rank this[string name]
        {
            get { return ranks.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>
        /// Creates rank set from enum.
        /// </summary>
        public static RankSet From<T>(Func<T, int> valueOf, T[] faced) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("not enum");
            }

            var result = new RankSet();
            foreach (T rank in Enum.GetValues(typeof(T)))
            {
                result.ranks.Add(new Rank(rank.ToString(), valueOf(rank), faced.Contains(rank)));
            }
            return result;
        }

        public Maybe<Rank> GetRank(string rankName)
        {
            var result = ranks.Find(x => x.Name == rankName);
            if (result.Equals(default(Rank)))
                return Maybe<Rank>.Nothing;

            return result.ToMaybe();
        }

        #region IReadOnlyList

        public int Count
        {
            get { return ranks.Count; }
        }

        public Rank this[int index]
        {
            get { return ranks[index]; }
        }

        public IEnumerator<Rank> GetEnumerator()
        {
            return ranks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ranks.GetEnumerator();
        }

        #endregion IReadOnlyList

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            // all unique
            Contract.Invariant(ranks.GroupBy(x => x).Count() == ranks.Count);
        }
    }
}