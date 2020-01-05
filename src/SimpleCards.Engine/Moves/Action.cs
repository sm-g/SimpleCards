using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    public class Action : SmartEnum<Action>
    {
        public static readonly Action PlayCard = new Action(nameof(PlayCard), 1);

        protected Action(string name, int value)
            : base(name, value)
        {
        }
    }
}