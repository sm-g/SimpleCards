using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleCards.Engine
{
    public abstract class TrickPile
    {
        public Dictionary<Party, List<Card>> AllCards => Dict;

        protected Dictionary<Party, List<Card>> Dict { get; } = new Dictionary<Party, List<Card>>();
        protected Party? Leader { get; set; }

        public abstract void Push(Card card, Party party);
    }

    public class PlainTrickPile : TrickPile
    {
        public IEnumerable<Card> Tricks => Dict.Values.SelectMany(x => x);

        public override void Push(Card card, Party party)
        {
            if (Dict.Count == 0)
                Leader = party;

            if (!Dict.TryGetValue(party, out var list))
                Dict[party] = list = new List<Card>();

            list.Add(card);
        }
    }

    public class TrickNDrawTrickPile : TrickPile
    {
        ////private List<Tuple<Card, Card>> q;

        public TrickNDrawTrickPile()
        {
        }

        public Party? Defense { get; set; }

        public void Push(Card card, Player player)
        {
            ////if (Defense.Players.Contains(p))
            ////{
            ////}
            //// base.Push(card, p);
        }

        public override void Push(Card card, Party party)
        {
            throw new NotImplementedException();
        }
    }
}