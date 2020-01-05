using System;
using System.Collections.Generic;
using System.Linq;

using Pastel;

using SimpleCards.Engine;

namespace SimpleCards.ExampleApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var suitset = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Hearts || s == FrenchSuits.Diamonds ? Color.Red : Color.Black);
            var rankset = RankSet.From<DefaultRanks>(r => (int)r, new[] { DefaultRanks.Jack, DefaultRanks.Queen, DefaultRanks.King });
            var rules = new Rules();
            var parties = CreateParties(2);
            var game = new Game(rankset, suitset, rules, parties);
            var dealer = new Dealer(game.Table, rules, game.Parties);
            game.Init();

            for (var gameNumber = 0; gameNumber < 2; gameNumber++)
            {
                // score incremented between games

                Log($"game #{gameNumber}");

                do
                {
                    // deal each round is optional
                    Log($"Begin round");
                    dealer.Deal();
                    game.RoundManager.BeginRound();

                    // every player make single move or until allowed

                    for (var moveNumver = 0; moveNumver < 4; moveNumver++)
                    {
                        Log($"move #{moveNumver}, wait move of {game.RoundManager.CurrentPlayer}");
                        ConsoleWriter.PrintHand(game.RoundManager.CurrentPlayer);

                        var movement = GetMovement(game.RoundManager.CurrentPlayer);

                        Log($"{game.RoundManager.CurrentPlayer} plays {ConsoleWriter.GetCardView(movement.Card!)}");
                        game.Move(movement);
                    }

                    Log($"End round ");
                    game.RoundManager.EndRound();
                }
                while (!rules.Ending.IsEnded(game));
            }
        }

        private static Movement GetMovement(Player currentPlayer)
        {
            Console.WriteLine($"enter command (0-based number of card to play)");
            var command = Console.ReadLine();
            var numberOfCardToPlay = int.Parse(command);

            // in UI new instance will be created
            var selectedCard = currentPlayer.Hand.ElementAt(numberOfCardToPlay);

            return new Movement(currentPlayer.Name, Engine.Action.PlayCard, selectedCard);
        }

        private static Parties CreateParties(int playersCount)
        {
            var parties = new List<Party>();
            for (var i = 1; i < playersCount + 1; i++)
            {
                var player = new Player("player" + i);
                var party = new Party("friends of " + player.Name);
                party.Players.Add(player);
                parties.Add(party);
            }

            return new Parties(parties);
        }

        private static void Log(string message) => Console.WriteLine("LOG: " + message);
    }

    internal static class ConsoleWriter
    {
        private static readonly Dictionary<Suit, string> SuitToPip = new Dictionary<Suit, string>
        {
            [new Suit(FrenchSuits.Hearts.ToString(), Color.Red)] = "♥".Pastel("ff0000"),
            [new Suit(FrenchSuits.Diamonds.ToString(), Color.Red)] = "♦".Pastel("ff0000"),
            [new Suit(FrenchSuits.Clubs.ToString(), Color.Black)] = "♣".Pastel("ffffff"),
            [new Suit(FrenchSuits.Spades.ToString(), Color.Black)] = "♠".Pastel("ffffff"),
        };

        public static void PrintHand(Player player)
        {
            var allCards = string.Join(",", player.Hand.Select(GetCardView));
            Console.WriteLine($"Hand of {player}: {allCards}");
        }

        public static string GetCardView(Card card)
        {
            var coloredSuit = SuitToPip[card.Suit];
            var rank = card.Rank.IsFace ? card.Rank.Name[0].ToString() : card.Rank.Value.ToString();
            return rank + coloredSuit;
        }
    }
}