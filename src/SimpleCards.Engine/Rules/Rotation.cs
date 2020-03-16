using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    /// <summary>
    /// The direction of dealing, bidding and playing.
    /// </summary>
    public class Rotation : SmartEnum<Rotation>
    {
        public static readonly Rotation Clockwise = new Rotation(nameof(Clockwise), 1);
        public static readonly Rotation Anticlockwise = new Rotation(nameof(Anticlockwise), 2);

        protected Rotation(string name, int value)
            : base(name, value)
        {
        }

        public int GetNextPlayerIndex(int currentIndex, int totalCount)
        {
            var delta = Equals(Clockwise) ? 1 : -1;
            return (currentIndex + delta) % totalCount;
        }
    }
}