using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Party
    {
        public Party(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("Not set", nameof(name));
            }

            Name = name;
            Players = new List<Player>();
        }

        public string Name { get; }
        public IList<Player> Players { get; }

        public override string ToString()
        {
            return $"{Name} party of {Players.Count}, first - {(Players.Any() ? Players[0].Name : "none")}";
        }
    }
}