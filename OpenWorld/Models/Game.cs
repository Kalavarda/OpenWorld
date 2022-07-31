using System;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models
{
    public class Game
    {
        public Hero.Hero Hero { get; }

        public Map Map { get; }

        public Game(Hero.Hero hero, Map map)
        {
            Hero = hero ?? throw new ArgumentNullException(nameof(hero));
            Map = map ?? throw new ArgumentNullException(nameof(map));
        }
    }
}
