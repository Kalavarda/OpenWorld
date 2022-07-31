using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace OpenWorld.Models.Hero
{
    public class HeroSpawn: IMapObject
    {
        public PointF Position { get; } = new();

        public BoundsF Bounds { get; }

        public HeroSpawn()
        {
            Bounds = new RoundBounds(Position, 0.5f);
        }
    }
}
