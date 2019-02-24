using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Party
    {
        public Party()
        {
            Players = new List<Player>();
        }

        public string Name { get; }
        public IList<Player> Players { get; }

        public override string ToString()
        {
            return string.Format("party of {0}, first {1}", Players.Count, Players.Any() ? Players[0].Name : "none");
        }
    }
}