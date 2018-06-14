using System.Collections.Generic;
using Value;

namespace SimpleCards.Engine
{
    public class Rank : ValueType<Rank>
    {
        public Rank(string name, int value, bool isFace = false)
        {
            Name = name;
            Value = value;
            IsFace = isFace;
        }

        public string Name { get; }

        /// <summary>
        /// Gets value of rank in game.
        ///
        /// <para>Ranks with same name have diff values in diff games.</para>
        /// </summary>
        public int Value { get; }

        public bool IsFace { get; }

        public override string ToString()
        {
            return $"{Name} ({Value})";
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] { Name, Value, IsFace };
        }
    }
}