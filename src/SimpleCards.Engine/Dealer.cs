using System.Linq;

namespace SimpleCards.Engine
{
    public class Dealer
    {
        public void Deal(Game game)
        {
            game.Pack = new Pack(game.SuitSet, game.RankSet);
            var stock = new Stock(game.Pack) { IsLastVisible = true };
            game.Table.Zones.Find(x => x.Name == "stock").Pile = stock;

            foreach (var player in game.Parties.SelectMany(x => x.Players))
            {
                var dealt = stock.Pop(PilePosition.Top, game.Rules.HandSize);
                player.Hand.Push(dealt, PilePosition.Top);
            }
        }
    }
}