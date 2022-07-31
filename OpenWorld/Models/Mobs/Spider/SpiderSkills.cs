using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;
using OpenWorld.Processes;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderAttack: ISkill, IDistanceSkill, IMakeSounds, IHasName
    {
        private const float AttackPower = 1;
        private const float PoisoingPower = 0.25f;

        private readonly Spider _spider;
        private readonly TimeLimiter _timeLimiter = new(TimeSpan.FromSeconds(10));

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
            IProcess process = null;

            _timeLimiter.Do(() =>
            {
                var changes = new UnitChanges(-AttackPower, this);
                Unit.Apply(_spider, changes, _spider.Target);

                PlaySound?.Invoke(nameof(SpiderAttack));

                process = new ChangeHpProcess(_spider, _spider.Target, -PoisoingPower, TimeSpan.FromSeconds(1), 10, BuffsRepository.Poisoning, this);
            });

            return process;
        }

        public event Action<string> PlaySound;
        
        public string Key => nameof(SpiderAttack);
    }
}
