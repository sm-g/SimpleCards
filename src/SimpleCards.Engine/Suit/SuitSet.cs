using System;
using System.Collections;
using System.Collections.Generic;
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

            if (!_suits.AllUnique(z => z.Name))
                throw new ArgumentException("Not unique names");
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

            var set = new List<Suit>();
            foreach (T suit in Enum.GetValues(typeof(T)))
            {
                set.Add(new Suit(suit.ToString(), colorOf(suit)));
            }
            return new SuitSet(set);
        }

        public Option<Suit> GetSuit(string suitName)
        {
            var result = _suits.Find(suit => suit.Name == suitName);

            return result.SomeNotNull();
        }

        public IEnumerator<Suit> GetEnumerator()
        {
            return _suits.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _suits.GetEnumerator();
        }
    }
}