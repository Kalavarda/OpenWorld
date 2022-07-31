using System;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Models.Hero;

namespace OpenWorld.Items
{
    internal class EquipmentItem: Item, IEquipmentItem, IUsable, IChangesModifier, IModifierParameters
    {
        private readonly EquipmentItemType _equipmentItemType;

        public EquipmentType EquipmentType { get; }

        public EquipmentItem(EquipmentItemType type) : base(type)
        {
            _equipmentItemType = type;
            EquipmentType = _equipmentItemType.EquipmentType;
        }

        public override Item Clone()
        {
            return new EquipmentItem(_equipmentItemType) { Count = Count };
        }

        public float? AttackRatio => _equipmentItemType.AttackRatio;

        public float? DefRatio => _equipmentItemType.DefRatio;

        public IProcess Use(ISkilled actor)
        {
            var hero = (Hero)actor;
            switch (EquipmentType)
            {
                case EquipmentType.Weapon:
                    hero.Equipment.Weapon = this;
                    break;

                case EquipmentType.Armor:
                    hero.Equipment.Armor = this;
                    break;

                case EquipmentType.Necklace:
                    hero.Equipment.Necklace = this;
                    break;

                default:
                    throw new NotImplementedException();
            }

            return null;
        }

        public void ChangeIncome(UnitChanges changes)
        {
            if (DefRatio != null)
                changes.HP /= DefRatio.Value;
        }

        public void ChangeOutcome(UnitChanges changes)
        {
            if (AttackRatio != null)
                changes.HP *= AttackRatio.Value;
        }
    }

    public enum EquipmentType
    {
        Weapon,
        Necklace,
        Armor
    }
}
