namespace SimpleCards.Engine
{
    public class Player
    {
        public Player(string name)
        {
            Hand = new Hand(this);
        }

        public string Name { get; set; }
        public Hand Hand { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}