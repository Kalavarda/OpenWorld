using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;

namespace OpenWorld.Models.Skills
{
    public class SimpleStrike: ISkill, IDistanceSkill, IMakeSounds, IHasKey
    {
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
                PlaySound?.Invoke(nameof(Hero) + nameof(SimpleStrike));
                _hero.Target.HP.Value -= 10;
            });

            return null;
        }

        public float MinDistance => 0;

        public float MaxDistance => 0.5f;

        public event Action<string> PlaySound;

        public string Key => nameof(SimpleStrike);
    }
}
