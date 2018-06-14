using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleCards.Engine
{
    class EmptyPileException : Exception
    {
        public Pile Pile { get; private set; }
        public EmptyPileException(Pile pile)
        {
            Pile = pile;
        }
    }
}
