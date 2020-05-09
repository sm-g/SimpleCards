using NUnit.Framework;

namespace SimpleCards.Engine.Tests.Tricks
{
    [TestFixture]
    public class TrumpTests
    {
        [Test]
        public void Card_never_trump_when_NoTrump_used()
        {
            var card = Stub.Cards.Rnd();
            var sut = Trump.NoTrump();

            Assert.False(sut.IsTrumpCard(card));
        }

        [Test]
        public void Card_with_suit_of_static_trump_is_trump_card()
        {
            var card = Stub.Cards.Rnd();
            var sut = Trump.Static(card.Suit);

            Assert.True(sut.IsTrumpCard(card));
        }

        [Test]
        public void Trump_card_beats_non_trump_card()
        {
            var ten = new Card(Stub.Ranks.Ten, Stub.Suits.Clubs);
            var sixTrump = new Card(Stub.Ranks.Six, Stub.Suits.Diamonds);
            var sut = Trump.Static(sixTrump.Suit);

            var result = sut.Compare(sixTrump, ten);

            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Trump_card_beats_other_trump_card()
        {
            var tenTrump = new Card(Stub.Ranks.Ten, Stub.Suits.Clubs);
            var sixTrump = new Card(Stub.Ranks.Six, Stub.Suits.Clubs);
            var sut = Trump.Static(sixTrump.Suit);

            var result = sut.Compare(tenTrump, sixTrump);

            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Non_trump_card_beats_other_non_trump_card()
        {
            var ten = new Card(Stub.Ranks.Ten, Stub.Suits.Clubs);
            var six = new Card(Stub.Ranks.Six, Stub.Suits.Clubs);
            var sut = Trump.Static(Stub.Suits.Spades);

            var result = sut.Compare(ten, six);

            Assert.That(result, Is.GreaterThan(0));
        }

        [Test]
        public void Non_trump_card_can_not_beat_other_non_trump_card_of_other_suit()
        {
            var ten = new Card(Stub.Ranks.Ten, Stub.Suits.Clubs);
            var six = new Card(Stub.Ranks.Six, Stub.Suits.Diamonds);
            var sut = Trump.Static(Stub.Suits.Spades);

            var result = sut.Compare(ten, six);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Card_can_not_beat_same_card()
        {
            var six = new Card(Stub.Ranks.Six, Stub.Suits.Clubs);
            var sut = Trump.Static(Stub.Suits.Spades);

            var result = sut.Compare(six, six);

            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public void Trump_card_can_not_beat_same_card()
        {
            var sixTrump = new Card(Stub.Ranks.Six, Stub.Suits.Clubs);
            var sut = Trump.Static(sixTrump.Suit);

            var result = sut.Compare(sixTrump, sixTrump);

            Assert.That(result, Is.EqualTo(0));
        }
    }
}