using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;

namespace OpenWorld.Models
{
    public abstract class Unit: IMapObject
    {
        public AngleF MoveDirection { get; } = new();

        public RangeF MoveSpeed { get; }

        public PointF Position { get; } = new();

        /// <summary>
        /// В какую точку нужно двигаться
        /// </summary>
        public PointF MoveTarget { get; } = new();

        protected Unit(RangeF moveSpeed)
        {
            MoveSpeed = moveSpeed;
        }

        public abstract BoundsF Bounds { get; }
    }
}
