using System;
using System.Windows.Media.Imaging;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
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

            var hero = new Hero(heroMoveSpeed);
            //hero.Bag.Add(new Item(ItemsRepository.WoodSword_Junk));
            //hero.Bag.Add(new Item(ItemsRepository.WoodSword_Ordinary));
            //hero.Bag.Add(new Item(ItemsRepository.WoodSword_Good));
            hero.Bag.Add(new Item(ItemsRepository.Chitin) { Count = 123 });
            hero.Bag.Add(new Item(ItemsRepository.HpPotion_Junk) { Count = 3 });

            return new Game(
                hero, 
                map);
        }

        private Map CreateMap()
        {
            var rand = new Random();
            var resAssembly = typeof(HeroControl).Assembly;

            var spawnsLayer = new MapLayer { IsHidden = true };
            for (var i = 0; i < 1000; i++)
            {
                var level = (ushort)rand.Next(1, 50);
                var r = 10 + 3 * level + 3 * rand.NextSingle();
                var a = 2 * MathF.PI * rand.NextSingle();
                var spawn = new SpiderSpawn(_levelMultiplier, level);
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
