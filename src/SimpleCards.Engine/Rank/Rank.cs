namespace SimpleCards.Engine
{
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
            return $"{Name} ({Value})";
        }
    }
}