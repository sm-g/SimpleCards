namespace SimpleCards.Engine
{
    public class ZoneFactory
    {
        public void CreateZones(Table table)
        {
            table.Zones.Add(Discard());
            table.Zones.Add(GameField());
            table.Zones.Add(Stock());
        }

        public Zone Discard()
        {
            return new Zone(Zone.DiscardName);
        }

        public Zone GameField()
        {
            return new Zone(Zone.GameFieldName);
        }

        public Zone Stock()
        {
            return new Zone(Zone.StockName);
        }
    }
}