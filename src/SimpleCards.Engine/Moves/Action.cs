using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    public class Action : SmartEnum<Action>
    {
        public static readonly Action PlayCard = new Action(nameof(PlayCard), 1);
        public static readonly Action Bid = new Action(nameof(Bid), 2);
        public static readonly Action TakeCards = new Action(nameof(TakeCards), 3);

        protected Action(string name, int value)
            : base(name, value)
        {
        }
    }
}