using System;
using System.Windows.Media.Imaging;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF;
using Kalavarda.Primitives.WPF.Map;
using OpenWorld.Controls;
using OpenWorld.Models;
using OpenWorld.Models.Hero;
using OpenWorld.Models.MapOjects;
using OpenWorld.Models.Mobs.Spider;

namespace OpenWorld.Factories
{
    internal class GameFactory
    {
        private readonly ILevelMultiplier _levelMultiplier;

        public GameFactory(ILevelMultiplier levelMultiplier)
        {
            _levelMultiplier = levelMultiplier ?? throw new ArgumentNullException(nameof(levelMultiplier));
        }

        public Game Create()
        {
            var heroMoveSpeed = new RangeF(0, 5_000 / 3600f);
            var map = CreateMap();

            return new Game(
                new Hero(heroMoveSpeed), 
                map);
        }

        private Map CreateMap()
        {
            var rand = new Random();
            var resAssembly = typeof(HeroControl).Assembly;

            var spawnsLayer = new MapLayer { IsHidden = true };
            for (var i = 0; i < 1000; i++)
            {
                var r = 10 + 20 * rand.NextSingle();
                var a = 2 * MathF.PI * rand.NextSingle();
                var spawn = new SpiderSpawn(_levelMultiplier);
                spawn.Position.Set(r * MathF.Cos(a), r * MathF.Sin(a));
                spawnsLayer.Add(spawn);
            }

            var heroRespawn = new HeroSpawn();
            spawnsLayer.Add(heroRespawn);

            var texturesLayer = new MapLayer();
            var texture = new MapTexture
            {
                Size = { Width = 200_000, Height = 200_000 },
                ImageSource = new BitmapImage(resAssembly.GetResourceUri("Images/Grass.jpg")),
                Scale = 1d / Settings.Default.GameControlScale
            };
            texturesLayer.Add(texture);

            var map = new Map();
            map.Add(texturesLayer);
            map.Add(new MapLayer());
            map.Add(spawnsLayer);
            return map;
        }
    }
}
