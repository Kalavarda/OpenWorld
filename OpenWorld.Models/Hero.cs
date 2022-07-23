using System.Collections.Generic;
using System.Linq;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;
using OpenWorld.Models.Skills;

namespace OpenWorld.Models
{
    public class Hero: Unit
    {
        private readonly ISkill[] _skills;

        public Hero(RangeF moveSpeed): base(moveSpeed)
        {
            Bounds = new RoundBounds(Position, 0.75f / 2);

            HP.Max = 100;
            HP.SetMax();

            _skills = new ISkill[]
            {
                new SimpleStrike(this)
            };
            foreach (var skill in _skills.OfType<IMakeSounds>())
                skill.PlaySound += RaisePlaySound;
        }

        public override BoundsF Bounds { get; }

        public override IEnumerable<ISkill> Skills => _skills;
    }
}
