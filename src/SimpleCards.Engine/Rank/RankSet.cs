using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Set of ranks in game.
    /// </summary>
    public class RankSet : ReadOnlyCollection<Rank>
    {
        public RankSet(IEnumerable<Rank> ranks)
            : base(ranks.ToList())
        {
            if (!Items.AllUnique(z => z.Value))
                throw new ArgumentException("Not unique values");
            if (!Items.AllUnique(z => z.Name))
                throw new ArgumentException("Not unique names");
        }

        public Rank? this[string name]
        {
            get { return Items.FirstOrDefault(x => x.Name.Equals(name)); }
        }

        public Rank GetWithValue(int value)
        {
            return Items.First(x => x.Value.Equals(value));
        }

        /// <summary>
        /// Creates rank set from enum.
        /// </summary>
        public static RankSet From<T>(Func<T, int> valueOf, T[] faced)
            where T : Enum
        {
            var set = Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new Rank(x.ToString(), valueOf(x), faced.Contains(x)));
            return new RankSet(set);
        }
    }
}