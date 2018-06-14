namespace SimpleCards.Engine
{
    public struct Suit
    {
        public Suit(string name)
            : this()
        {
            Name = name;
        }

        public Suit(string name, int color)
            : this()
        {
            Name = name;
            Color = color;
        }

        public string Name { get; private set; }
        public int Color { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}