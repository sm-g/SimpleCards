using System;

namespace SimpleCards.Engine
{
    public class EmptyPileException : Exception
    {
        public Pile Pile { get; private set; }

        public EmptyPileException(Pile pile)
        {
            Pile = pile;
        }
    }
}