using System.Collections.Generic;
using System.Linq;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Mobs.Spider
{
    public class Spider: Mob
    {
        private readonly ISkill[] _skills;

        public Spider(RangeF moveSpeed, Spawn spawn) : base(moveSpeed, spawn)
        {
            Bounds = new RoundBounds(Position, 0.3f);
            
            HP.Max = 10;
            HP.SetMax();

            _skills = new ISkill[]
            {
                new SpiderAttack(this)
            };
            foreach (var skill in _skills.OfType<IMakeSounds>())
                skill.PlaySound += RaisePlaySound;
        }

        public override BoundsF Bounds { get; }

        public override IEnumerable<ISkill> Skills => _skills;

        public override float AggrDistance => 5;

        public override float MaxDistanceFromSpawn => 2 * AggrDistance;

        public override IProcess CreateFightProcess(IProcessor processor)
        {
            return new MobFightProcess(this, processor);
        }
    }
}
