using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Units;
using OpenWorld.Models;
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
            var rand = new Random();
            var spawnsLayer = new MapLayer { IsHidden = true };
            for (var i = 0; i < 20; i++)
            {
                var r = 2 + 10 * rand.NextSingle();
                var a = MathF.PI * rand.NextSingle();
                var spawn = new SpiderSpawn();
                spawn.Position.Set(r * MathF.Cos(a), r * MathF.Sin(a));
                spawnsLayer.Add(spawn);
            }

            var map = new Map();
            map.Add(new MapLayer());
            map.Add(spawnsLayer);
            return map;
        }
    }
}
