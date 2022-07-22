using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderAttack: ISkill, IDistanceSkill
    {
        public string Name => "Укус";

        public float MinDistance => 0;

        public float MaxDistance => 1;

        public ITimeLimiter TimeLimiter { get; } = new TimeLimiter(TimeSpan.FromSeconds(1));

        public IProcess Use(ISkilled initializer)
        {
            if (initializer is Mob mob)
            {
                mob.Target.HP.Value -= 1;
                return null;
            }
            else
                throw new NotImplementedException();
        }
    }
}
