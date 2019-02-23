using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    public class Color : SmartEnum<Color>
    {
        public static readonly Color Black = new Color(nameof(Black), 1);
        public static readonly Color Red = new Color(nameof(Red), 2);

        protected Color(string name, int value)
            : base(name, value)
        {
        }
    }
}