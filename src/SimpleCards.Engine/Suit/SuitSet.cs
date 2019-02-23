using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Optional;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Set of suits used in game.
    /// </summary>
    public class SuitSet : ReadOnlyCollection<Suit>
    {
        /// <summary>
        /// Creates suit set from list of suits.
        /// </summary>
        public SuitSet(IEnumerable<Suit> suits)
            : base(suits.ToList())
        {
            if (!Items.AllUnique(z => z.Name))
                throw new ArgumentException("Not unique names");
        }

        public Suit this[string name]
        {
            get { return Items.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)); }
        }

        /// <summary>
        /// Creates suit set from enum.
        /// </summary>
        public static SuitSet From<T>(Func<T, Color> colorOf) where T : Enum
        {
            var set = Enum
                .GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new Suit(x.ToString(), colorOf(x)));
            return new SuitSet(set);
        }

        public Option<Suit> GetSuit(string suitName)
        {
            return Items.FirstOrDefault(x => x.Name.Equals(suitName, StringComparison.OrdinalIgnoreCase)).SomeNotNull();
        }
    }
}