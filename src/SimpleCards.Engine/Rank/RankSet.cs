using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Optional;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Set of ranks in game.
    /// </summary>
    public class RankSet : IReadOnlyList<Rank>
    {
        private readonly List<Rank> _ranks = new List<Rank>();

        public RankSet(IEnumerable<Rank> ranks)
        {
            _ranks = new List<Rank>(ranks);
        }

        private RankSet()
        {
        }

        public Rank this[string name]
        {
            get { return _ranks.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
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
                result._ranks.Add(new Rank(rank.ToString(), valueOf(rank), faced.Contains(rank)));
            }
            return result;
        }

        public Option<Rank> GetRank(string rankName)
        {
            var result = _ranks.Find(x => x.Name == rankName);

            return result.SomeNotNull();
        }

        #region IReadOnlyList

        public int Count
        {
            get { return _ranks.Count; }
        }

        public Rank this[int index]
        {
            get { return _ranks[index]; }
        }

        public IEnumerator<Rank> GetEnumerator()
        {
            return _ranks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _ranks.GetEnumerator();
        }

        #endregion IReadOnlyList

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            // all unique
            Contract.Invariant(_ranks.GroupBy(x => x).Count() == _ranks.Count);
        }
    }
}