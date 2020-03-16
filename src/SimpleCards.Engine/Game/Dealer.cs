using System;
using System.Diagnostics;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Dealer
    {
        private readonly Table _table;
        private readonly Rules _rules;
        private readonly Parties _parties;

        public Dealer(Table table, Rules rules, Parties parties)
        {
            _table = table ?? throw new ArgumentNullException(nameof(table));
            _rules = rules ?? throw new ArgumentNullException(nameof(rules));
            _parties = parties ?? throw new ArgumentNullException(nameof(parties));
        }

        public void Deal()
        {
            var allCardsForNextGame = CollectAllCards();
            allCardsForNextGame.Shuffle();

            if (allCardsForNextGame.IsEmpty)
                throw new InvalidOperationException("There is no cards to be used in next deal");

            HandOut(allCardsForNextGame);

            PutRestOnStock(allCardsForNextGame);
        }

        private Pile CollectAllCards()
        {
            var result = _table.Collect();

            // why Dealer take cards from players?
            // maybe players should put their cards on Table before dealing?
            foreach (var player in _parties.Players)
            {
                result.Push(player.Hand.PopAll(), PilePosition.Default);
            }

            return result;
        }

        private void HandOut(Pile allCardsForNextGame)
        {
            var totalHands = _parties.Players.Count();
            if (allCardsForNextGame.Size < totalHands * _rules.HandSize)
            {
                throw new InvalidOperationException($"Not enough free cards ({allCardsForNextGame.Size}) to hand out between {totalHands} players");
            }

            foreach (var player in _parties.Players)
            {
                var dealtPacket = allCardsForNextGame.Pop(PilePosition.Top, _rules.HandSize);

                if (dealtPacket.Count < _rules.HandSize)
                    throw new NotImplementedException("Can not deal at end of game");

                Debug.Assert(player.Hand.IsEmpty, "hand not empty before HandOut");
                player.Hand.Push(dealtPacket, PilePosition.Default);
            }
        }

        private void PutRestOnStock(Pile allCardsForNextGame)
        {
            if (allCardsForNextGame.IsEmpty)
                return;

            var stockZone = _table.Stock;
            if (stockZone == null)
            {
                throw new InvalidOperationException($"There is no Stock in current game, the rest ({allCardsForNextGame.Size}) of collected cards lost");
            }

            var stock = new Stock(allCardsForNextGame)
            {
                IsLastVisible = true
            };
            stockZone.PlacePile(stock);
        }
    }
}