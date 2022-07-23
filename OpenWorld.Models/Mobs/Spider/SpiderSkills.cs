using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderAttack: ISkill, IDistanceSkill, IMakeSounds
    {
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

        public IProcess Use()
        {
            _timeLimiter.Do(() =>
            {
                PlaySound?.Invoke(nameof(SpiderAttack));

                _spider.Target.HP.Value -= 2;
            });

            return null;
        }

        public event Action<string> PlaySound;
    }
}
