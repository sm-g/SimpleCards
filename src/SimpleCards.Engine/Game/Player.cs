namespace SimpleCards.Engine
{
    public class Player
    {
        public Player(string name)
        {
            Name = new Name(name);
            Hand = new Hand(this);
        }

        public Name Name { get; }
        public Hand Hand { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}