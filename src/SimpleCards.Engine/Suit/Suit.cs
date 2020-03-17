using System.Collections.Generic;

using ByValue;

namespace SimpleCards.Engine
{
    public class Suit : ValueObject
    {
        public Suit(string name, Color color)
        {
            Name = new Name(name);
            Color = color;
        }

        public Name Name { get; }
        public Color Color { get; }

        public override string ToString() => Name;

        protected override IEnumerable<object> Reflect()
        {
            return new object[] { Name, Color };
        }
    }
}