using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace OpenWorld.Models
{
    public class Spawn: IHasPosition
    {
        public PointF Position { get; } = new();

        public float Radius { get; set; }
    }
}
