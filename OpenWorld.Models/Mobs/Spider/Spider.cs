using System.Collections.Generic;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Mobs.Spider
{
    public class Spider: Mob
    {
        private readonly ISkill[] _skills =
        {
            new SpiderAttack()
        };

        public Spider(RangeF moveSpeed, Spawn spawn) : base(moveSpeed, spawn)
        {
            Bounds = new RoundBounds(Position, 0.3f);
            
            HP.Max = 2;
            HP.SetMax();
        }

        public override BoundsF Bounds { get; }

        public override IEnumerable<ISkill> Skills => _skills;

        public override float AggrDistance => 10;

        public override float MaxDistanceFromSpawn => 20;

        public override IProcess CreateFightProcess(IProcessor processor)
        {
            return new MobFightProcess(this, processor);
        }
    }
}
