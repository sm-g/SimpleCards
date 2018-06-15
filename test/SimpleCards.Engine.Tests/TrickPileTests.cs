using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class TrickPileTests
    {
        [Test]
        public void PushMiddle_NeverTopOrBottom()
        {
            var p = new Party();
            var p2 = new Party();
            var pile = new PlainTrickPile();
            var card = RndCard(1);
            var card2 = RndCard(2);

            pile.Push(card, p);
            pile.Push(card2, p2);

            Assert.Fail();
        }

        //[Test]
        //public void PushMiddle_()
        //{
        //    var p = new Party();
        //    var p2 = new Party();
        //    var pile = new TrickNDrawTrickPile();
        //    var card = RndCard(1);
        //    var card2 = RndCard(2);

        //    pile.Push(card, p);
        //    pile.Push(card2, p2);

        //    pile.
        //}

        private static Card RndCard(int i = 1)
        {
            return new Card(new Rank("1", i), new Suit("1"));
        }
    }
}