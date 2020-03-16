using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Party
    {
        public Party(string name)
        {
            Name = new Name(name);
            Players = new List<Player>();
        }

        public Name Name { get; }
        public IList<Player> Players { get; }

        public override string ToString()
        {
            return $"{Name} party of {Players.Count}, first - {(Players.Any() ? Players[0].Name : "none")}";
        }
    }
}