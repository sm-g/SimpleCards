using System.Linq;

namespace SimpleCards.Engine
{
    public class Dealer
    {
        public void Deal(Game game)
        {
            var allCardsForNextGame = game.Table.Collect();
            foreach (var player in game.Parties.SelectMany(x => x.Players))
            {
                allCardsForNextGame.Push(player.Hand.PopAll(), PilePosition.Bottom);
            }

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
            game.Table.Zones.Find(x => x.Name == Zone.StockName).Pile = stock;
        }
    }
}