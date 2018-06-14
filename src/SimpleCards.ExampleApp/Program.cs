using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleCards.Engine;

namespace SimpleCards.Tester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var suitset = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Clubs || s == FrenchSuits.Diamonds ? 1 : 0);
            var rankset = RankSet.From<DefaultRanks>(r => (int)r, new[] { DefaultRanks.Jack, DefaultRanks.Queen, DefaultRanks.King });
            var rules = new Rules() { PackSize = 36 };
            var game = new Game(rankset, suitset, rules, 2);

            new ZoneFactory().CreateZones(game);
            new Starter().BeginRound(game);
            new Dealer().Deal(game);
        }
    }
}