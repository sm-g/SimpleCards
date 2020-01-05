using System;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Zone can have different purpose, known by <see cref="Rules"/> and <see cref="AI"/>.
    /// </summary>
    public class Zone
    {
        public const string DiscardName = "discard";
        public const string GameFieldName = "field";
        public const string StockName = "stock";

        public Zone(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Not set", nameof(name));
            }

            Name = name;
            Pile = new Pile();
        }

        public string Name { get; }

        public Pile Pile { get; private set; }

        /// <summary>
        /// Sets pile on zone explicitly.
        /// </summary>
        public void PlacePile(Pile pile)
        {
            if (!Pile.IsEmpty)
                throw new InvalidOperationException("Current pile of this zone is not empty");

            Pile = pile;
        }
    }
}