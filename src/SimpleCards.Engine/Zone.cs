using System;

namespace SimpleCards.Engine
{
    /// <summary>
    /// Zone can have different purpose, known by <see cref="Rules"/> and <see cref="AI"/>.
    /// </summary>
    public class Zone
    {
        public static readonly Name DiscardName = new Name("discard");
        public static readonly Name GameFieldName = new Name("field");
        public static readonly Name StockName = new Name("stock");

        public Zone(string name)
        {
            Name = new Name(name);
            Pile = new Pile();
        }

        public Name Name { get; }

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