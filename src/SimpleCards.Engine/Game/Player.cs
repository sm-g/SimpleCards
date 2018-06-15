namespace SimpleCards.Engine
{
    public class Player
    {
        public Player(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("Null or empty", nameof(name));
            }

            Name = name;
            Hand = new Hand(this);
        }

        public string Name { get; }
        public Hand Hand { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}