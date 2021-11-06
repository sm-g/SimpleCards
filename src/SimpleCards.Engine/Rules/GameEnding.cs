using System;
using System.Linq;

using Ardalis.SmartEnum;

namespace SimpleCards.Engine
{
    public abstract class GameEnding : SmartEnum<GameEnding>
    {
        public static readonly GameEnding FirstPlayerWithEmptyHand = new FirstEmptyGameEnding(nameof(FirstPlayerWithEmptyHand), 1);
        public static readonly GameEnding SinglePlayerWithHand = new SingleWithCardsGameEnding(nameof(SinglePlayerWithHand), 2);
        public static readonly GameEnding NoCardsInHands = new NoCardsInHandsGameEnding(nameof(NoCardsInHands), 3);
        public static readonly GameEnding NoCardsInStock = new NoCardsInStockGameEnding(nameof(NoCardsInStock), 4);

        protected GameEnding(string name, int value)
            : base(name, value)
        {
        }

        public abstract bool IsEnded(Game game);

        private sealed class FirstEmptyGameEnding : GameEnding
        {
            public FirstEmptyGameEnding(string name, int value)
                : base(name, value)
            {
            }

            public override bool IsEnded(Game game)
            {
                return game.Parties.Players.Any(x => x.Hand.IsEmpty);
            }
        }

        private sealed class SingleWithCardsGameEnding : GameEnding
        {
            public SingleWithCardsGameEnding(string name, int value)
                : base(name, value)
            {
            }

            public override bool IsEnded(Game game)
            {
                return game.Parties.Players.Where(x => !x.Hand.IsEmpty).Count() == 1;
            }
        }

        private sealed class NoCardsInHandsGameEnding : GameEnding
        {
            public NoCardsInHandsGameEnding(string name, int value)
                : base(name, value)
            {
            }

            public override bool IsEnded(Game game)
            {
                return game.Parties.Players.All(x => x.Hand.IsEmpty);
            }
        }

        private sealed class NoCardsInStockGameEnding : GameEnding
        {
            public NoCardsInStockGameEnding(string name, int value)
                : base(name, value)
            {
            }

            public override bool IsEnded(Game game)
            {
                var stock = game.Table.Stock;
                if (stock == null)
                    throw new InvalidOperationException($"There is no {nameof(Table.Stock)} in game");

                return stock.Pile.IsEmpty;
            }
        }
    }
}