using System;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Items;

namespace OpenWorld.Models.Items
{
    public class HpPotion: Item, IUsable
    {
        public HpPotion(ItemType type) : base(type)
        {
        }

        public IProcess Use(ISkilled actor)
        {
            if (actor is Unit unit)
                return new ChangeHpProcess(unit, unit, 1f, TimeSpan.FromSeconds(1), 10);
            else
                throw new NotSupportedException();
        }
    }
}
