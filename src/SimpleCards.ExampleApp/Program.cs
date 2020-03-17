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
            var rankset = RankSet.From<DurakRanks>(r => (int)r, new[] { DurakRanks.Jack, DurakRanks.Queen, DurakRanks.King });
            var rules = new Rules();
            var parties = CreateParties(2);
            var game = new Game(rankset, suitset, rules, parties);
            var dealer = new Dealer(game.Table, rules, game.Parties);
            game.Init();

            for (var dealNumber = 0; dealNumber < 2; dealNumber++)
            {
                // score incremented between deals

                Log($"deal #{dealNumber}");

                dealer.Deal();
                var roundNumber = 0;
                do
                {
                    Log($"Begin round #{roundNumber}");
                    game.Round.Begin();

                    // assume single movement per player

                    // every player make single PlayCard move or until allowed

                    for (var moveNumver = 0; moveNumver < 4; moveNumver++)
                    {
                        Log($"move #{moveNumver}, wait move of {game.Round.CurrentPlayer}");
                        ConsoleWriter.PrintHand(game.Round.CurrentPlayer);

                        var movement = GetMovement(game.Round.CurrentPlayer);

                        Log($"{game.Round.CurrentPlayer} plays {ConsoleWriter.GetCardView(movement.Card!)}");
                        game.Move(movement);
                    }

                    Log($"End round #{roundNumber++}");
                    ConsoleWriter.PrintTable(game.Table, Log);

                    game.Round.End();
                }
                while (!rules.Ending.IsEnded(game));

                Log($"deal #{dealNumber} ended");
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

        public static void PrintTable(Table table, Action<string> printer)
        {
            foreach (var zone in table.Zones)
            {
                printer($"{zone.Name}: {zone.Pile}");
            }
        }

        public static string GetCardView(Card card)
        {
            var coloredSuit = SuitToPip[card.Suit];
            var rank = GetRankView(card.Rank);
            return rank + coloredSuit;
        }

        // not is Rank itself, depends on game
        private static string GetRankView(Rank rank) => rank.IsFace || rank.Value == 14
            ? rank.Name.Initial
            : rank.Value.ToString();
    }
}