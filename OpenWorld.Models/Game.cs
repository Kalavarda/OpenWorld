﻿using System;

namespace OpenWorld.Models
{
    public class Game
    {
        public Hero Hero { get; }

        public Map Map { get; }

        public Game(Hero hero, Map map)
        {
            Hero = hero ?? throw new ArgumentNullException(nameof(hero));
            Map = map ?? throw new ArgumentNullException(nameof(map));
        }
    }
}
