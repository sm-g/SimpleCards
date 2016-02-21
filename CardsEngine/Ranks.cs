using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Functional.Maybe;

namespace SimpleCards.Engine
{
    public enum DefaultRanks
    {
        Ace = 1,
        Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Jack, Queen, King
    }

    public struct Rank
    {
        public Rank(string name, int value, bool isFace = false)
            : this()
        {
            Name = name;
            Value = value;
            IsFace = isFace;
        }

        public string Name { get; private set; }

        /// <summary>
        /// Gets value of rank in game.
        ///
        /// <para>Ranks with same name have diff values in diff games.</para>
        /// </summary>
        public int Value { get; private set; }

        public bool IsFace { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} ({1})", Name, Value);
        }
    }

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

        public int Count
        {
            get { return ranks.Count; }
        }

        public Rank this[int index]
        {
            get { return ranks[index]; }
        }

        public Rank this[string name]
        {
            get { return ranks.Find(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>
        /// Creates rank set from enum.
        /// </summary>
        public static RankSet From<T>(Func<T, int> valueOf) where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("not enum");
            }

            var result = new RankSet();
            foreach (T rank in Enum.GetValues(typeof(T)))
            {
                result.ranks.Add(new Rank(rank.ToString(), valueOf(rank)));
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

        public IEnumerator<Rank> GetEnumerator()
        {
            return ranks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ranks.GetEnumerator();
        }

        [ContractInvariantMethod]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Required for code contracts.")]
        private void ObjectInvariant()
        {
            // all unique
            Contract.Invariant(ranks.GroupBy(x => x).Count() == ranks.Count);
        }
    }
}