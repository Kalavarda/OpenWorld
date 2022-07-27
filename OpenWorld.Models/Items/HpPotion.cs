using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units.Items;

namespace OpenWorld.Models.Items
{
    public class HpPotion: Item, IUsable
    {
        public HpPotion(ItemType type) : base(type)
        {
        }

        public IProcess Use()
        {
            throw new System.NotImplementedException();
        }
    }
}
