using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;

namespace OpenWorld.Models.Hero.Skills
{
    public class SimpleStrike: ISkill, IDistanceSkill, IMakeSounds, IHasName
    {
        private const int AttackPower = 1;

        private readonly TimeLimiter _timeLimiter = new(TimeSpan.FromSeconds(1));

        public string Name => "Простой удар";

        public ITimeLimiter TimeLimiter => _timeLimiter;

        public bool CanUse(ISkilled actor)
        {
            if (_timeLimiter.Remain > TimeSpan.Zero)
                return false;

            if (actor is Unit unit)
            {
                if (unit.Target == null)
                    return false;

                var distance = unit.Position.DistanceTo(unit.Target.Position);
                if (distance > MaxDistance)
                    return false;

                return true;
            }

            return false;
        }

        public IProcess Use(ISkilled actor)
        {
            if (!CanUse(actor))
                return null;

            if (actor is Hero hero)
                _timeLimiter.Do(() =>
                {
                    var distance = hero.Position.DistanceTo(hero.Target.Position);
                    if (distance > MaxDistance)
                        return;

                    var changes = new UnitChanges(-AttackPower, this);
                    Unit.Apply(hero, changes, (IFighter)hero.Target);

                    PlaySound?.Invoke(nameof(Hero) + nameof(SimpleStrike));
                });

            return null;
        }

        public float MinDistance => 0;

        public float MaxDistance => 0.5f;

        public event Action<string> PlaySound;

        public string Key => nameof(SimpleStrike);
    }
}
