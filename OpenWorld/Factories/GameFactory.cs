﻿using System;
using System.Reflection;
using System.Windows.Media.Imaging;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF;
using Kalavarda.Primitives.WPF.Map;
using OpenWorld.Controls;
using OpenWorld.Models;
using OpenWorld.Models.Mobs.Spider;

namespace OpenWorld.Factories
{
    internal class GameFactory
    {
        public Game Create()
        {
            var heroMoveSpeed = new RangeF(0, 5_000 / 3600f);
            var map = CreateMap();

            return new Game(
                new Hero(heroMoveSpeed), 
                map);
        }

        private static Map CreateMap()
        {
            var rand = new Random();
            var resAssembly = typeof(HeroControl).Assembly;

            var spawnsLayer = new MapLayer { IsHidden = true };
            for (var i = 0; i < 1; i++)
            {
                var r = 5 + 5 * rand.NextSingle();
                var a = 2 * MathF.PI * rand.NextSingle();
                var spawn = new SpiderSpawn();
                spawn.Position.Set(r * MathF.Cos(a), r * MathF.Sin(a));
                spawnsLayer.Add(spawn);
            }

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
