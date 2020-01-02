﻿using System;

using SimpleCards.Engine;

namespace SimpleCards.Tester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var suitset = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Clubs || s == FrenchSuits.Diamonds ? Color.Red : Color.Black);
            var rankset = RankSet.From<DefaultRanks>(r => (int)r, new[] { DefaultRanks.Jack, DefaultRanks.Queen, DefaultRanks.King });
            var rules = new Rules();
            var game = new Game(rankset, suitset, rules, 2);
            var dealer = new Dealer(game);
            game.Init();

            for (var gameNumber = 0; gameNumber < 2; gameNumber++)
            {
                // score incremented between games

                Console.WriteLine($"game #{gameNumber}");

                dealer.Deal();
                const int Rounds = 2;
                for (var roundNumber = 0; roundNumber < Rounds; roundNumber++)
                {
                    Console.WriteLine($"Begin round {roundNumber}");

                    for (var moveNumver = 0; moveNumver < 3; moveNumver++)
                    {
                        Console.WriteLine($"move #{moveNumver}");

                        game.Move();
                    }
                }
            }
        }
    }
}