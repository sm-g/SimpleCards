using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCards.Engine
{
    public class Zone
    {
        public Zone()
        {
            Pile = new Pile();
        }

        public string Name { get; set; }
        public Pile Pile { get; set; }
    }
}