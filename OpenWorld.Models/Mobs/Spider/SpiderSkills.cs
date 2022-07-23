using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderAttack: ISkill, IDistanceSkill
    {
        private readonly TimeLimiter _timeLimiter = new(TimeSpan.FromSeconds(1));

        public string Name => "Укус";

        public float MinDistance => 0;

        public float MaxDistance => 1;

        public ITimeLimiter TimeLimiter => _timeLimiter;

        public IProcess Use(ISkilled initializer)
        {
            IProcess process = null;

            if (initializer is Mob mob)
                _timeLimiter.Do(() =>
                {
                    mob.Target.HP.Value -= 2;
                    //process = null;
                });
            else
                throw new NotImplementedException();

            return process;
        }
    }
}
