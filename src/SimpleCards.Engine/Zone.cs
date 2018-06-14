namespace SimpleCards.Engine
{
    public class Zone
    {
        public const string DiscardName = "discard";
        public const string GameFieldName = "field";
        public const string StockName = "stock";

        public Zone()
        {
            Pile = new Pile();
        }

        public string Name { get; set; }
        public Pile Pile { get; set; }
    }
}