using System;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class UseItemController : IUseItemController, IDisposable
    {
        private readonly IProcessor _processor;
        private readonly Hero _hero;

        public UseItemController(IProcessor processor, Hero hero)
        {
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));

            _hero.Equipment.Changed += Equipment_Changed;
        }

        private void Equipment_Changed(Equipment arg1, Items.EquipmentType arg2, IEquipmentItem oldEqItem, IEquipmentItem newEqItem)
        {
            if (oldEqItem != null)
                _hero.Bag.Add((Item)oldEqItem);
        }

        public void Use(IUsable usable)
        {
            var process = usable.Use(_hero);
            if (process != null)
                _processor.Add(process);
        }

        public void Dispose()
        {
            _hero.Equipment.Changed -= Equipment_Changed;
        }
    }
}
