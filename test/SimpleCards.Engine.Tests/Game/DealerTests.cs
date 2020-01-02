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
        public void Stock_remains_empty_when_all_cards_in_hands()
        {
            var cards = _f.CreateMany<Card>(10).ToList();
            var table = new Table(new ZoneFactory());
            table.GameField.Pile.Push(cards, PilePosition.Default);
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
            var cards = _f.CreateMany<Card>(10).ToList();
            var table = new Table(new ZoneFactory());
            table.GameField.Pile.Push(cards, PilePosition.Default);
            var rules = new Rules()
            {
                HandSize = 6
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            var ex = Assert.Catch<NotImplementedException>(() => sut.Deal());
            Assert.That(ex.Message, Contains.Substring("").IgnoreCase);
        }

        [Test]
        public void Should_throw_when_there_is_no_stock_zone_on_table_and_some_cards_not_handed_out()
        {
            var cards = _f.CreateMany<Card>(10).ToList();
            var table = new Table(new ZoneFactoryMock());
            table.GameField.Pile.Push(cards, PilePosition.Default);
            var rules = new Rules()
            {
                HandSize = 4
            };
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            var ex = Assert.Catch<InvalidOperationException>(() => sut.Deal());
            Assert.That(ex.Message, Contains.Substring("no Stock in current game").IgnoreCase);
        }

        [Test]
        public void Should_throw_when_there_is_no_free_cards()
        {
            var table = new Table(new ZoneFactoryMock());
            var rules = new Rules();
            var parties = CreateParties(2);
            var sut = new Dealer(table, rules, parties);

            var ex = Assert.Catch<InvalidOperationException>(() => sut.Deal());
            Assert.That(ex.Message, Contains.Substring("no cards to be used in next deal").IgnoreCase);
        }

        private static List<Party> CreateParties(int playersCount)
        {
            var parties = new List<Party>();
            for (var i = 1; i < playersCount + 1; i++)
            {
                var player = new Player("player" + i);
                var party = new Party("friends of " + player.Name);
                party.Players.Add(player);
                parties.Add(party);
            }

            return parties;
        }

        private class ZoneFactoryMock : ZoneFactory
        {
            public override List<Zone> CreateZones()
            {
                return new List<Zone> { GameField() };
            }
        }
    }
}