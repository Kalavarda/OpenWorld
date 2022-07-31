using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderAttack: ISkill, IDistanceSkill, IMakeSounds, IHasName
    {
        private const int AttackPower = 1;

        private readonly Spider _spider;
        private readonly TimeLimiter _timeLimiter = new(TimeSpan.FromSeconds(1));

        public string Name => "Укус";

        public float MinDistance => 0;

        public float MaxDistance => 0.5f;

        public ITimeLimiter TimeLimiter => _timeLimiter;

        public SpiderAttack(Spider spider)
        {
            _spider = spider ?? throw new ArgumentNullException(nameof(spider));
        }

        public IProcess Use(ISkilled actor)
        {
            _timeLimiter.Do(() =>
            {
                var changes = new UnitChanges(-AttackPower, this);
                Unit.Apply(_spider, changes, _spider.Target);

                PlaySound?.Invoke(nameof(SpiderAttack));
            });

            return null;
        }

        public event Action<string> PlaySound;
        
        public string Key => nameof(SpiderAttack);
    }
}
