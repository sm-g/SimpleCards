namespace SimpleCards.Engine
{
    public class ZoneFactory
    {
        public void CreateZones(Game game)
        {
            game.Table.Zones.Add(Discard());
            game.Table.Zones.Add(GameField());
            game.Table.Zones.Add(Stock());
        }

        public Zone Discard()
        {
            return new Zone() { Name = Zone.DiscardName };
        }

        public Zone GameField()
        {
            return new Zone() { Name = Zone.GameFieldName };
        }

        public Zone Stock()
        {
            return new Zone() { Name = Zone.StockName };
        }
    }
}