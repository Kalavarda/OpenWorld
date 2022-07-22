using Kalavarda.Primitives;
using OpenWorld.Models;
using OpenWorld.Models.Mobs;
using OpenWorld.Models.Mobs.Spider;

namespace OpenWorld.Factories
{
    internal class GameFactory
    {
        public Game Create()
        {
            var heroMoveSpeed = new RangeF(0, 10_000 / 3600f);
            var map = CreateMap();

            return new Game(
                new Hero(heroMoveSpeed), 
                map);
        }

        private static Map CreateMap()
        {
            var spawn1 = new Spawn();
            spawn1.Position.Set(3, 2);
            spawn1.Radius = 0.5f;

            var spawn2 = new Spawn();
            spawn2.Position.Set(4, 3);
            spawn2.Radius = 0.5f;

            var mobsLayer = new MapLayer();
            var spider1 = new Spider(new RangeF { Max = 0.5f }, spawn1);
            spider1.Position.Set(spawn1.Position);
            mobsLayer.Add(spider1);
            var spider2 = new Spider(new RangeF { Max = 0.5f }, spawn2);
            spider2.Position.Set(spawn2.Position);
            mobsLayer.Add(spider2);

            var map = new Map();
            map.Add(mobsLayer);
            return map;
        }
    }
}
