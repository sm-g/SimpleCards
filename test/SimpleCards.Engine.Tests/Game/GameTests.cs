using System;

using NUnit.Framework;

namespace SimpleCards.Engine
{
    [TestFixture]
    public class GameTests
    {
        private readonly SuitSet _suitset;
        private readonly RankSet _rankset;

        public GameTests()
        {
            _suitset = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Hearts || s == FrenchSuits.Diamonds ? Color.Red : Color.Black);
            _rankset = new RankSet(new[]
            {
                new Rank("r1", 1),
                new Rank("r2", 2),
                new Rank("r3", 3),
                new Rank("r4", 4, true)
            });
        }

        [Test]
        public void Ctor_should_ensure_all_players_can_begin_with_full_hand()
        {
            var rules = new Rules()
            {
                HandSize = 4
            };
            var parties = Build.Parties(5);

            var ex = Assert.Catch<ArgumentException>(() => new Game(_rankset, _suitset, rules, parties));
            Assert.That(ex.Message, Contains.Substring("Too many players").IgnoreCase);
            Assert.That(ex.ParamName, Is.EqualTo("parties"));
        }
    }
}