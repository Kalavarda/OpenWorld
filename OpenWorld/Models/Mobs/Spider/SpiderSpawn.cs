using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;

namespace OpenWorld.Models.Mobs.Spider
{
    public class SpiderSpawn: Spawn
    {
        private readonly ILevelMultiplier _levelMultiplier;
        private readonly ushort _level;

        public SpiderSpawn(ILevelMultiplier levelMultiplier, ushort level) : base(1, TimeSpan.FromSeconds(30))
        {
            _levelMultiplier = levelMultiplier ?? throw new ArgumentNullException(nameof(levelMultiplier));
            _level = level;
        }

        protected override Unit CreateUnit()
        {
            var spider = new Spider(new RangeF { Max = 2.0f }, this) { Level = _level };
            spider.HP.Max = _levelMultiplier.GetValue(spider.HP.Max, spider.Level);
            spider.HP.SetMax();
            spider.AttackRatio = _levelMultiplier.GetValue(1, spider.Level);
            spider.DefRatio = _levelMultiplier.GetValue(1, spider.Level);
            return spider;
        }
    }
}
