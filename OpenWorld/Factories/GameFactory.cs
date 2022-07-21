using Kalavarda.Primitives;
using OpenWorld.Models;
using OpenWorld.Models.Mobs;

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
            var mobsLayer = new MapLayer();
            var spider1 = new Spider(new RangeF { Max = 0.5f });
            spider1.Position.Set(3, 2);
            mobsLayer.Add(spider1);
            var spider2 = new Spider(new RangeF { Max = 0.5f });
            spider2.Position.Set(4, 3);
            mobsLayer.Add(spider2);

            var map = new Map();
            map.Add(mobsLayer);
            return map;
        }
    }
}
