namespace SimpleCards.Engine
{
    public class ZoneFactory
    {
        public void CreateZones(Game game)
        {
            game.Table.Zones.Add(Discard());
            game.Table.Zones.Add(Gamefield());
            game.Table.Zones.Add(Stock());
        }

        public Zone Discard()
        {
            return new Zone() { Name = "discard" };
        }

        public Zone Gamefield()
        {
            return new Zone() { Name = "field" };
        }

        public Zone Stock()
        {
            return new Zone() { Name = "stock" };
        }
    }
}