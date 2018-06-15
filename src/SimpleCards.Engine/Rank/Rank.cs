using System;
using System.Collections.Generic;
using Value;

namespace SimpleCards.Engine
{
#pragma warning disable S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()

    public class Rank : ValueType<Rank>, IComparable<Rank>
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

        public int CompareTo(Rank other) => Comparison(this, other);

        public static int Comparison(Rank x, Rank y)
        {
            if (x is null && y is null)
                return 0;
            if (y is null)
                return 1;
            if (x is null)
                return -1;

            return x.Value.CompareTo(y.Value);
        }

        public static bool operator >(Rank x, Rank y) => Comparison(x, y) > 0;

        public static bool operator >=(Rank x, Rank y) => Comparison(x, y) >= 0;

        public static bool operator <(Rank x, Rank y) => Comparison(x, y) < 0;

        public static bool operator <=(Rank x, Rank y) => Comparison(x, y) <= 0;

        public static bool operator ==(Rank x, Rank y) => Comparison(x, y) == 0;

        public static bool operator !=(Rank x, Rank y) => Comparison(x, y) != 0;

        public override string ToString()
        {
            return $"{Name} ({Value})";
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] { Name, Value, IsFace };
        }
    }

#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning restore S1210 // "Equals" and the comparison operators should be overridden when implementing "IComparable"
}