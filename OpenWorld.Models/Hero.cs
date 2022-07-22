using System;
using System.Collections.Generic;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Skills;

namespace OpenWorld.Models
{
    public class Hero: Unit
    {
        public Hero(RangeF moveSpeed): base(moveSpeed)
        {
            Bounds = new RoundBounds(Position, 0.75f / 2);

            HP.Max = 100;
            HP.SetMax();
        }

        public override BoundsF Bounds { get; }

        public override IEnumerable<ISkill> Skills => Array.Empty<ISkill>();
    }
}
