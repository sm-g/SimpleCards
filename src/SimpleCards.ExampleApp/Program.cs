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
            var rules = new Rules();
            var game = new Game(rankset, suitset, rules, 2);
            game.Init();

            for (var gameNumber = 0; gameNumber < 2; gameNumber++)
            {
                // score incremented between games

                Console.WriteLine($"game #{gameNumber}");

                rules.Dealer.Deal(game);
                const int rounds = 2;
                for (var i = 0; i < rounds; i++)
                {
                    Console.WriteLine($"Begin round {i}");

                    for (var k = 0; k < 3; k++)
                    {
                        Console.WriteLine($"move #{k}");

                        game.Move();
                    }
                }
            }
        }
    }
}