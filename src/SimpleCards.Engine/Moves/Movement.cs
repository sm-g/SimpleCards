namespace SimpleCards.Engine
{
    public class Movement
    {
        // MAYBE subclasess for Movement by Action? not to have nullable Card

        public Movement(Name playerName, Action action, Card? card = null)
        {
            PlayerName = playerName;
            Action = action;
            Card = card;
        }

        public Name PlayerName { get; }
        public Action Action { get; }
        public Card? Card { get; }
    }
}