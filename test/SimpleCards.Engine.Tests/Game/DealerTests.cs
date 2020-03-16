using System;
using System.Collections.Generic;
using System.Linq;

using AutoFixture;

using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class DealerTests
    {
        private readonly Fixture _f;

        public DealerTests()
        {
            _f = new Fixture();
            _f.Register(() => Color.Black);
        }

        [Test]
        public void Collects_all_cards_from_the_table()
        {
            var table = new Table(new ZoneFactory());
            ScatterCardsOnTable(table, 10);
            var rules = new Rules()
            {
                HandSize = 5
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            sut.Deal();

            CollectionAssert.IsEmpty(table.GameField.Pile);
            CollectionAssert.IsEmpty(table.Stock.Pile);
            CollectionAssert.IsEmpty(table.Discard.Pile);
        }

        [Test]
        public void Collects_all_cards_from_players_hands()
        {
            var table = new Table(new ZoneFactory());
            ScatterCardsOnTable(table, 10);
            var rules = new Rules()
            {
                HandSize = 5
            };
            var parties = CreateParties(2);
            parties[0].Players[0].Hand.Push(_f.Create<Card>(), PilePosition.Default);
            parties[1].Players[0].Hand.Push(_f.Create<Card>(), PilePosition.Default);
            var sut = new Dealer(table, rules, parties);

            sut.Deal();

            // TODO how to assert this? same card may be again in same hand
            Assert.Fail();
        }

        [Test]
        public void Hands_out_cards_to_players()
        {
            var table = new Table(new ZoneFactory());
            ScatterCardsOnTable(table, 25);
            var rules = new Rules()
            {
                HandSize = 5
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            sut.Deal();

            Assert.AreEqual(5, parties[0].Players[0].Hand.Size);
            Assert.AreEqual(5, parties[1].Players[0].Hand.Size);
        }

        [Test]
        public void Puts_rest_of_cards_on_stock()
        {
            var table = new Table(new ZoneFactory());
            ScatterCardsOnTable(table, 25);
            var rules = new Rules()
            {
                HandSize = 5
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            sut.Deal();

            Assert.AreEqual(15, table.Stock.Pile.Size);
            Assert.IsInstanceOf<Stock>(table.Stock.Pile);
            var stockPile = table.Stock.Pile as Stock;
            Assert.IsTrue(stockPile.IsLastVisible);
        }

        [Test]
        public void Stock_remains_empty_when_all_cards_in_hands()
        {
            var table = new Table(new ZoneFactory());
            ScatterCardsOnTable(table, 10);
            var rules = new Rules()
            {
                HandSize = 5
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            sut.Deal();

            Assert.IsTrue(table.Stock.Pile.IsEmpty);
        }

        [Test]
        public void Should_throw_when_not_enough_free_cards_to_hand_out()
        {
            var table = new Table(new ZoneFactory());
            ScatterCardsOnTable(table, 10);
            var rules = new Rules()
            {
                HandSize = 6
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            var ex = Assert.Catch<InvalidOperationException>(() => sut.Deal());
            Assert.That(ex.Message, Contains.Substring("Not enough free cards").IgnoreCase);
        }

        [Test]
        public void Should_throw_when_there_is_no_stock_zone_on_table_and_some_cards_not_handed_out()
        {
            var table = new Table(new NoStockZoneFactory());
            ScatterCardsOnTable(table, 10);
            var rules = new Rules()
            {
                HandSize = 1
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            var ex = Assert.Catch<InvalidOperationException>(() => sut.Deal());
            Assert.That(ex.Message, Contains.Substring("no Stock in current game").IgnoreCase);
        }

        [Test]
        public void Should_throw_when_there_is_no_free_cards()
        {
            var table = new Table(new ZoneFactory());
            var rules = new Rules();
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            var ex = Assert.Catch<InvalidOperationException>(() => sut.Deal());
            Assert.That(ex.Message, Contains.Substring("no cards to be used in next deal").IgnoreCase);
        }

        private static Parties CreateParties(int playersCount)
        {
            var parties = new List<Party>();
            for (var i = 1; i < playersCount + 1; i++)
            {
                var player = new Player("player" + i);
                var party = new Party("friends of " + player.Name);
                party.Players.Add(player);
                parties.Add(party);
            }

            return new Parties(parties);
        }

        private void ScatterCardsOnTable(Table table, int count)
        {
            var cards = _f.CreateMany<Card>(count).ToList();
            if (cards.Count >= 4)
            {
                table.GameField.Pile.Push(cards.Take(4).ToList(), PilePosition.Default);
            }
            if (cards.Count > 4)
            {
                table.Stock?.Pile.Push(cards.Skip(4).Take(3).ToList(), PilePosition.Default);
            }
            if (cards.Count > 7)
            {
                table.Discard?.Pile.Push(cards.Skip(7).ToList(), PilePosition.Default);
            }
        }

        private class NoStockZoneFactory : ZoneFactory
        {
            public override List<Zone> CreateZones()
            {
                return new List<Zone> { GameField() };
            }
        }
    }
}