using System;
using System.Linq;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using OpenWorld.Models;
using OpenWorld.Models.Hero;
using OpenWorld.Models.MapOjects;

namespace OpenWorld.Controllers
{
    internal class HeroRespawnController: IDisposable
    {
        private readonly Hero _hero;
        private readonly Map _map;

        public HeroRespawnController(Hero hero, Map map)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _map = map ?? throw new ArgumentNullException(nameof(map));

            _hero.Died += Hero_Died;
        }

        private void Hero_Died(ICreature hero)
        {
            var nearestSpawn = _map.Layers
                .SelectMany(l => l.Objects)
                .OfType<HeroSpawn>()
                .MinBy(s => _hero.Position.DistanceTo(s.Position));
            _hero.Position.Set(nearestSpawn.Position);
            _hero.HP.SetMax();
        }

        public void Dispose()
        {
            _hero.Died -= Hero_Died;
        }
    }
}
