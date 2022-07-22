using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderSpawn: Spawn
    {
        public SpiderSpawn() : base(1)
        {
        }

        protected override Unit CreateUnit()
        {
            return new Spider(new RangeF { Max = 0.5f }, this);
        }
    }
}
