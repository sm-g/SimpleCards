using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class FollowSuitTests
    {
        [Test]
        [TestCaseSource(typeof(FollowSuit), nameof(FollowSuit.List))]
        public void Any_card_could_be_played_when_trick_pile_is_empty(FollowSuit sut)
        {
            var trickPile = new TrickPile(sut, Trump.NoTrump());
            var hand = CreateHandWithCards(Stub.Suits.Clubs);
            var card = hand.CardsInPile[0];

            var result = sut.Play(trickPile, hand, card);

            Assert.AreEqual(FollowSuitPlayResult.Played, result);
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void Any_card_could_be_played_with_FollowSuit_None(int suit)
        {
            var sut = FollowSuit.None;
            var trickPile = new TrickPile(sut, Trump.NoTrump());
            trickPile.Push(new Card(Stub.Ranks.Six, Stub.Suits.French[0]), PilePosition.Default);
            var hand = CreateHandWithCards(Stub.Suits.French[suit]);
            var card = hand.CardsInPile[0];

            var result = sut.Play(trickPile, hand, card);

            Assert.AreEqual(FollowSuitPlayResult.Played, result);
        }

        [Test]
        public void Play_other_suit_when_can_follow_suit()
        {
            var sut = FollowSuit.Regular;
            var trickPile = CreateTrickPileWithSixClubs(sut);
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Diamonds);
            var otherSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Revoke, result);
        }

        [Test]
        public void Play_other_trump_suit_when_can_follow_suit()
        {
            var sut = FollowSuit.Regular;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Spades);
            var otherTrumpSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherTrumpSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Revoke, result);
        }

        [Test]
        [TestCase(nameof(FollowSuit.Regular))]
        [TestCase(nameof(FollowSuit.MustTrump))]
        public void Play_trump_suit_when_pile_has_only_trumps(string followSuit)
        {
            var sut = FollowSuit.FromName(followSuit);
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Clubs));
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Hearts);
            var sameSuitCard = hand.CardsInPile[0];

            var result = sut.Play(trickPile, hand, sameSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Played, result);
        }

        [Test]
        [TestCase(nameof(FollowSuit.Regular))]
        [TestCase(nameof(FollowSuit.MustTrump))]
        public void Play_other_trump_suit_when_cannot_follow_suit(string followSuit)
        {
            var sut = FollowSuit.FromName(followSuit);
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            var hand = CreateHandWithCards(Stub.Suits.Hearts, Stub.Suits.Spades);
            var otherTrumpSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherTrumpSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Ruff, result);
        }

        [Test]
        public void Play_other_plain_suit_when_cannot_follow_suit()
        {
            var sut = FollowSuit.Regular;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            var hand = CreateHandWithCards(Stub.Suits.Hearts, Stub.Suits.Diamonds, Stub.Suits.Spades);
            var otherPlainSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherPlainSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Slough, result);
        }

        [Test]
        public void Play_higher_trump_to_beat_when_pile_already_has_trump_not_following_suit()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            trickPile.Push(new Card(Stub.Ranks.Six, Stub.Suits.Spades), PilePosition.Top);
            var hand = CreateHandWithCards(Stub.Suits.Hearts, Stub.Suits.Spades);
            var otherTrumpSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherTrumpSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Ruff, result);
        }

        [Test]
        public void Play_higher_trump_to_beat_when_pile_already_has_trump_not_following_suit_and_can_follow_initial_suit()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            trickPile.Push(new Card(Stub.Ranks.Six, Stub.Suits.Spades), PilePosition.Top);
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Spades);
            var otherTrumpSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherTrumpSuitCard);

            // not sure, maybe Played?
            Assert.AreEqual(FollowSuitPlayResult.Ruff, result);
        }

        [Test]
        public void Play_lower_trump_when_pile_already_has_trump_not_following_suit_and_can_follow_initial_suit()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            trickPile.Push(new Card(Stub.Ranks.Default[11], Stub.Suits.Spades), PilePosition.Top);
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Spades);
            var otherTrumpSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherTrumpSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Ruff, result);
        }

        // must beat any trump card already played to the trick
        [Test]
        public void Play_lower_trump_when_pile_already_has_trump_not_following_suit_and_can_beat_played_trump()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            trickPile.Push(new Card(Stub.Ranks.Six, Stub.Suits.Spades), PilePosition.Top);
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Spades);
            hand.Push(new Card(Stub.Ranks.Default[2], Stub.Suits.Spades), PilePosition.Default);
            var otherTrumpSuitCard = hand.CardsInPile[2];

            var result = sut.Play(trickPile, hand, otherTrumpSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Revoke, result);
        }

        [Test]
        public void Play_lower_trump_when_pile_already_has_trump_not_following_suit_and_can_not_follow_initial_suit()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            trickPile.Push(new Card(Stub.Ranks.Default[11], Stub.Suits.Spades), PilePosition.Top);
            var hand = CreateHandWithCards(Stub.Suits.Hearts, Stub.Suits.Spades);
            var otherTrumpSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherTrumpSuitCard);

            // Maybe Played?
            Assert.AreEqual(FollowSuitPlayResult.Ruff, result);
        }

        [Test]
        public void Play_other_not_trump_suit_when_pile_already_has_trump_not_following_suit_and_can_follow_initial_suit()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            trickPile.Push(new Card(Stub.Ranks.Default[11], Stub.Suits.Spades), PilePosition.Top);
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Diamonds, Stub.Suits.Spades);
            var otherSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Revoke, result);
        }

        [Test]
        public void Play_other_not_trump_suit_when_can_trump_to_beat()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Diamonds, Stub.Suits.Spades);
            var otherSuitCard = hand.CardsInPile[1];

            var result = sut.Play(trickPile, hand, otherSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Revoke, result);
        }

        [Test]
        public void Play_initial_not_trump_suit_when_can_trump()
        {
            var sut = FollowSuit.MustTrump;
            var trickPile = CreateTrickPileWithSixClubs(sut, Trump.Static(Stub.Suits.Spades));
            var hand = CreateHandWithCards(Stub.Suits.Clubs, Stub.Suits.Diamonds, Stub.Suits.Spades);
            var otherSuitCard = hand.CardsInPile[0];

            var result = sut.Play(trickPile, hand, otherSuitCard);

            Assert.AreEqual(FollowSuitPlayResult.Played, result);
        }

        private static TrickPile CreateTrickPileWithSixClubs(FollowSuit sut)
        {
            var trickPile = new TrickPile(sut, Trump.NoTrump());
            trickPile.Push(new Card(Stub.Ranks.Six, Stub.Suits.Clubs), PilePosition.Default);
            return trickPile;
        }

        private static TrickPile CreateTrickPileWithSixClubs(FollowSuit sut, Trump trump)
        {
            var trickPile = new TrickPile(sut, trump);
            trickPile.Push(new Card(Stub.Ranks.Six, Stub.Suits.Clubs), PilePosition.Default);
            return trickPile;
        }

        private static Hand CreateHandWithCards(params Suit[] suits)
        {
            var hand = new Hand(Build.Parties(1).Players[0]);
            foreach (var suit in suits)
            {
                hand.Push(new Card(Stub.Ranks.Ten, suit), PilePosition.Default);
            }
            return hand;
        }
    }
}