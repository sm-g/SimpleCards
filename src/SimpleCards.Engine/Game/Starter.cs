using System.Linq;

namespace SimpleCards.Engine
{
    public class Starter
    {
        /// <summary>
        /// Starts new round from scratch (with zero points etc).
        /// </summary>
        public void BeginRound(Game game)
        {
            game.StartPlayers();

            // TODO on new round cards recreated?
            game.Table.Collect();
            foreach (var player in game.Parties.SelectMany(x => x.Players))
            {
                player.Hand.Pop(PilePosition.Top, player.Hand.Size);
            }
        }

        /// <summary>
        /// Starts new round with existing players and score history.
        /// </summary>
        public void BeginRoundCont(Game game)
        {
            game.Table.Collect();
            foreach (var player in game.Parties.SelectMany(x => x.Players))
            {
                player.Hand.Pop(PilePosition.Top, player.Hand.Size);
            }
        }
    }
}