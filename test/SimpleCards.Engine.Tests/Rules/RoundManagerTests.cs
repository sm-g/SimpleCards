using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class RoundManagerTests
    {
        private static readonly Rules Rules = new Rules()
        {
            HandSize = 4
        };

        [Test]
        public void CurrentPlayer_is_the_eldest_after_begin()
        {
            var table = Build.Table();
            var parties = Build.Parties(2);
            var sut = new RoundManager(table, Rules, parties);

            sut.BeginRound();

            Assert.AreSame(parties[0].Players[0], sut.CurrentPlayer);
        }

        [Test]
        public void CurrentPlayer_changes_by_rotation_after_move()
        {
            var table = Build.Table();
            var parties = Build.Parties(2);
            var sut = new RoundManager(table, Rules, parties);
            sut.BeginRound();

            sut.OnMove(new Movement((Name)"player", Action.Bid));

            Assert.AreSame(parties[1].Players[0], sut.CurrentPlayer);
        }

        [Test]
        public void CurrentPlayer_is_the_eldest_again_after_whole_round()
        {
            var table = Build.Table();
            var parties = Build.Parties(2);
            var sut = new RoundManager(table, Rules, parties);
            sut.BeginRound();

            sut.OnMove(new Movement((Name)"player1", Action.Bid));
            sut.OnMove(new Movement((Name)"player2", Action.Bid));

            Assert.AreSame(parties[0].Players[0], sut.CurrentPlayer);
        }

        [Test]
        public void Players_hands_filled_with_cards_from_stock_after_end()
        {
            var table = Build.Table();
            var parties = Build.Parties(2);
            var sut = new RoundManager(table, Rules, parties);
            sut.BeginRound();
            sut.OnMove(new Movement((Name)"player1", Action.PlayCard));
            sut.OnMove(new Movement((Name)"player2", Action.PlayCard));

            sut.EndRound();

            Assert.AreEqual(Rules.HandSize, parties[0].Players[0].Hand.Size);
            Assert.AreEqual(Rules.HandSize, parties[1].Players[0].Hand.Size);
        }
    }
}