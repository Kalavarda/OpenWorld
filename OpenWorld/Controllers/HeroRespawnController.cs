using System;
using System.Linq;
using System.Timers;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class HeroRespawnController: IDisposable
    {
        private readonly Hero _hero;
        private readonly Map _map;
        private Timer _timer;

        public HeroRespawnController(Hero hero, Map map)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _map = map ?? throw new ArgumentNullException(nameof(map));

            _hero.Died += Hero_Died;
        }

        private void Hero_Died(ICreature hero)
        {
            _timer = new Timer { Interval = Settings.Default.HeroDeathDuration.TotalMilliseconds };
            _timer.Elapsed += Timer_Elapsed;
            _timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();

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
