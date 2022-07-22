using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderAttack: ISkill, IDistanceSkill
    {
        public string Name => "Укус";

        public float MinDistance => 0;

        public float MaxDistance => 1.5f;

        public ITimeLimiter TimeLimiter { get; } = new TimeLimiter(TimeSpan.FromSeconds(1));

        public IProcess Use(ISkilled initializer)
        {
            if (initializer is Mob mob)
            {
                mob.Target.HP.Value -= 10;
                return null;
            }
            else
                throw new NotImplementedException();
        }
    }
}
