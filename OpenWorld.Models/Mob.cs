using Kalavarda.Primitives;

namespace OpenWorld.Models
{
    public abstract class Mob: Unit
    {
        protected Mob(RangeF moveSpeed) : base(moveSpeed)
        {
        }
    }
}
