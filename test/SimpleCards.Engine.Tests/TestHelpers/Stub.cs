namespace SimpleCards.Engine
{
    internal static class Stub
    {
        public static class Cards
        {
            public static Card Rnd(int rank = 1) => new Card(Ranks.Default.GetWithValue(rank), Suits.Clubs);
        }

        public static class Ranks
        {
            public static readonly RankSet Default = RankSet.From(r => (int)r, new[] { DefaultRanks.Jack, DefaultRanks.Queen, DefaultRanks.King });

            public static Rank Six => Default[5];
            public static Rank Ten => Default[9];
        }

        public static class Suits
        {
            public static readonly SuitSet French = SuitSet.From<FrenchSuits>(s => s == FrenchSuits.Hearts || s == FrenchSuits.Diamonds ? Color.Red : Color.Black);

#nullable disable
            public static Suit Hearts => French[nameof(FrenchSuits.Hearts)];
            public static Suit Diamonds => French[nameof(FrenchSuits.Diamonds)];
            public static Suit Clubs => French[nameof(FrenchSuits.Clubs)];
            public static Suit Spades => French[nameof(FrenchSuits.Spades)];
#nullable restore
        }
    }
}