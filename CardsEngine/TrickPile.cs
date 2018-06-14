using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public abstract class TrickPile
    {
        protected Dictionary<Party, List<Card>> dict = new Dictionary<Party, List<Card>>();
        protected Party leader;

        public Dictionary<Party, List<Card>> AllCards { get { return dict; } }

        public abstract void Push(Card card, Party p);
    }

    public class PlainTrickPile : TrickPile
    {
        public IEnumerable<Card> Tricks { get { return dict.Values.SelectMany(x => x); } }

        public override void Push(Card card, Party p)
        {
            if (dict.Count == 0)
                leader = p;

            List<Card> list;
            if (!dict.TryGetValue(p, out list))
                dict[p] = (list = new List<Card>());

            list.Add(card);
        }
    }

    public class TrickNDrawTrickPile : TrickPile
    {
        private List<Tuple<Card, Card>> q;

        public TrickNDrawTrickPile()
        {
        }

        public Party Defense { get; set; }

        public void Push(Card card, Player p)
        {
            if (Defense.Players.Contains(p))
            {
            }
            // base.Push(card, p);
        }

        public override void Push(Card card, Party p)
        {
            throw new NotImplementedException();
        }
    }
}