using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;

namespace OpenWorld.Models.Mobs
{
    public class Spider: Mob
    {
        public Spider(RangeF moveSpeed) : base(moveSpeed)
        {
            Bounds = new RoundBounds(Position, 0.2f);
        }

        public override BoundsF Bounds { get; }
    }
}
