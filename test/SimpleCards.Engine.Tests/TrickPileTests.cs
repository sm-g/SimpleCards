using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class TrickPileTests
    {
        [Test]
        public void PushMiddle_NeverTopOrBottom()
        {
            var p = new Party("good boys");
            var p2 = new Party("bad boys");
            var pile = new PlainTrickPile();
            var card = RndCard(1);
            var card2 = RndCard(2);

            pile.Push(card, p);
            pile.Push(card2, p2);

            Assert.Fail();
        }

        private static Card RndCard(int i = 1)
        {
            return new Card(new Rank("1", i), new Suit("1", Color.Black));
        }
    }
}