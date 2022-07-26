using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Hero
{
    public class SimpleStrike: ISkill, IDistanceSkill, IMakeSounds
    {
        private const int AttackPower = 1;

        private readonly Hero _hero;
        private readonly TimeLimiter _timeLimiter = new(TimeSpan.FromSeconds(1));

        public SimpleStrike(Hero hero)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
        }

        public string Name => "Простой удар";

        public ITimeLimiter TimeLimiter => _timeLimiter;

        public IProcess Use()
        {
            _timeLimiter.Do(() =>
            {
                var distance = _hero.Position.DistanceTo(_hero.Target.Position);
                if (distance > MaxDistance)
                    return;

                var changes = new UnitChanges(-AttackPower);
                Unit.Apply(_hero, changes, _hero.Target);

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
