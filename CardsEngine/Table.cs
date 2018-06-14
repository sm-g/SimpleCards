using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCards.Engine
{
    public class Table
    {
        public Table()
        {
            Zones = new List<Zone>();
        }
        public List<Zone> Zones { get; set; }

        public void Clear()
        {
            foreach (var z in Zones)
            {
                z.Pile.Clear();
            }
        }
    }
}
