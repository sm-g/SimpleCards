using System.Linq;

namespace SimpleCards.Engine
{
    public class Dealer
    {
        public void Deal(Game game)
        {
            var allCardsForNextGame = CollectAllCards(game);
            allCardsForNextGame.Shuffle();

            if (allCardsForNextGame.IsEmpty)
            {
                // first game
                allCardsForNextGame = game.Rules.MaterializeRequiredPack(game.SuitSet, game.RankSet);
            }

            foreach (var player in game.Parties.SelectMany(x => x.Players))
            {
                var dealt = allCardsForNextGame.Pop(PilePosition.Top, game.Rules.HandSize);
                player.Hand.Push(dealt, PilePosition.Top);
            }

            var stock = new Stock(allCardsForNextGame) { IsLastVisible = true };
            var stockPileOnTable = game.Table.Zones.Find(x => x.Name == Zone.StockName).Pile;
            stockPileOnTable.Push(stock, PilePosition.Bottom);
        }

        private static Pile CollectAllCards(Game game)
        {
            var result = game.Table.Collect();
            foreach (var player in game.Parties.SelectMany(x => x.Players))
            {
                result.Push(player.Hand.PopAll(), PilePosition.Bottom);
            }

            return result;
        }
    }
}