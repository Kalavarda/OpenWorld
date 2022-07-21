using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;

namespace OpenWorld.Models
{
    public class Hero: Unit
    {
        public Hero(RangeF moveSpeed): base(moveSpeed)
        {
            Bounds = new RoundBounds(Position, 0.75f / 2);
        }

        public override BoundsF Bounds { get; }
    }
}
