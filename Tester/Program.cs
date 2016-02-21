using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCards.Engine;

namespace SimpleCards.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            SuitSet suitset = SuitSet.From<FrenchSuits>();
            RankSet rankset = RankSet.From<DefaultRanks>(r => (int)r);

            Card a = new Card(rankset[0], suitset[0]);
            Card b = new Card(rankset[0], suitset[0]);
            Card c = new Card(rankset[1], suitset["Clubs"]);

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);

            Pile pile = new Pile(suitset, rankset, false);
            Console.WriteLine("pile:");
            Console.WriteLine(pile.ToString());

            Console.WriteLine("after shuffle:");
            pile.Shuffle();
            Console.WriteLine(pile.ToString());
            Console.Read();
        }
    }
}
