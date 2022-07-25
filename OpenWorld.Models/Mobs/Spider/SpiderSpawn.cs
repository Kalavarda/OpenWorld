using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderSpawn: Spawn
    {
        private readonly ILevelMultiplier _levelMultiplier;

        public SpiderSpawn(ILevelMultiplier levelMultiplier) : base(1)
        {
            _levelMultiplier = levelMultiplier ?? throw new ArgumentNullException(nameof(levelMultiplier));
        }

        protected override Unit CreateUnit()
        {
            var spider = new Spider(new RangeF { Max = 1.0f }, this) { Level = 1 };
            spider.HP.Max = _levelMultiplier.GetValue(spider.HP.Max, spider.Level);
            spider.HP.SetMax();
            return spider;
        }
    }
}
