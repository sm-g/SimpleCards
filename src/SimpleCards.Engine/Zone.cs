namespace SimpleCards.Engine
{
    public class Zone
    {
        public const string DiscardName = "discard";
        public const string GameFieldName = "field";
        public const string StockName = "stock";

        public Zone(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new System.ArgumentException("Not set", nameof(name));
            }

            Name = name;
            Pile = new Pile();
        }

        public string Name { get; }
        public Pile Pile { get; }
    }
}