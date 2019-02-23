using System.Collections.Generic;
using ByValue;

namespace SimpleCards.Engine
{
    public class Suit : ValueObject
    {
        public Suit(string name)
        {
            Name = name;
        }

        public Suit(string name, int color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; }
        public int Color { get; }

        public override string ToString()
        {
            return Name;
        }

        protected override IEnumerable<object> Reflect()
        {
            return new object[] { Name, Color };
        }
    }
}