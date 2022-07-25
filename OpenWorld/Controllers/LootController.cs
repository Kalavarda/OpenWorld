using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.EventAggregators;
using Kalavarda.Primitives.Units.Interfaces;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    public class LootController: IDisposable
    {
        private readonly Hero _hero;
        private readonly ICreatureEventAggregator _eventAggregator;
        private readonly ILevelMultiplier _levelMultiplier;

        private void Award(Mob mob)
        {
            _hero.XP.Value += _levelMultiplier.GetValue(1, mob.Level);
        }

        public LootController(Hero hero, ICreatureEventAggregator eventAggregator, ILevelMultiplier levelMultiplier)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _levelMultiplier = levelMultiplier ?? throw new ArgumentNullException(nameof(levelMultiplier));

            _eventAggregator.Died += Mob_Died;
        }

        private void Mob_Died(ICreature obj)
        {
            if (obj is Mob mob)
                Award(mob);
        }

        public void Dispose()
        {
            _eventAggregator.Died -= Mob_Died;
        }
    }
}
