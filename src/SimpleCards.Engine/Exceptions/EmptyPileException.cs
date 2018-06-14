using System;

namespace SimpleCards.Engine
{
    public class EmptyPileException : InvalidOperationException
    {
        public EmptyPileException(Pile pile)
        {
            Pile = pile;
        }

        public Pile Pile { get; }
    }
}