namespace SimpleCards.Engine
{
    public class Movement
    {
        public Movement(string playerName, Action action, Card? card = null)
        {
            PlayerName = playerName;
            Action = action;
            Card = card;
        }

        public string PlayerName { get; }
        public Action Action { get; }
        public Card? Card { get; }
    }
}