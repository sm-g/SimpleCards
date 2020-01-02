using System;
using System.Diagnostics;
using System.Linq;

namespace SimpleCards.Engine
{
    public class Dealer
    {
        private readonly Game _game;

        public Dealer(Game game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
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
            var result = _game.Table.Collect();
            foreach (var player in _game.Parties.SelectMany(x => x.Players))
            {
                result.Push(player.Hand.PopAll(), PilePosition.Default);
            }

            return result;
        }

        private void HandOut(Pile allCardsForNextGame)
        {
            foreach (var player in _game.Parties.SelectMany(x => x.Players))
            {
                var dealtPacket = allCardsForNextGame.Pop(PilePosition.Top, _game.Rules.HandSize);

                Debug.Assert(player.Hand.IsEmpty, "hand not empty before HandOut");
                player.Hand.Push(dealtPacket, PilePosition.Default);
            }
        }

        private void PutRestOnStock(Pile allCardsForNextGame)
        {
            if (allCardsForNextGame.IsEmpty)
                return;

            var stockZone = _game.Table.Stock;
            if (stockZone == null)
            {
                throw new InvalidOperationException("There is no Stock in current game, the rest of collected cards lost");
            }

            var stock = new Stock(allCardsForNextGame)
            {
                IsLastVisible = true
            };
            stockZone.Pile.Push(stock, PilePosition.Bottom);
        }
    }
}