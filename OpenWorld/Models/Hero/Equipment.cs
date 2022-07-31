using System;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Items;

namespace OpenWorld.Models.Hero
{
    public class Equipment: IChangesModifier
    {
        private IEquipmentItem _weapon;
        private IEquipmentItem _armor;
        private IEquipmentItem _necklace;

        public IEquipmentItem Weapon
        {
            get => _weapon;
            set
            {
                if (_weapon == value)
                    return;

                var oldValue = _weapon;
                _weapon = value;
                Changed?.Invoke(this, EquipmentType.Weapon, oldValue, value);
            }
        }

        public IEquipmentItem Armor
        {
            get => _armor;
            set
            {
                if (_armor == value)
                    return;

                var oldValue = _armor;
                _armor = value;
                Changed?.Invoke(this, EquipmentType.Armor, oldValue, value);
            }
        }

        public IEquipmentItem Necklace
        {
            get => _necklace;
            set
            {
                if (_necklace == value)
                    return;

                var oldValue = _necklace;
                _necklace = value;
                Changed?.Invoke(this, EquipmentType.Necklace, oldValue, value);
            }
        }

        public event Action<Equipment, EquipmentType, IEquipmentItem, IEquipmentItem> Changed;

        public void ChangeIncome(UnitChanges changes)
        {
            if (Weapon is IChangesModifier weapon)
                weapon.ChangeIncome(changes);

            if (Armor is IChangesModifier armor)
                armor.ChangeIncome(changes);

            if (Necklace is IChangesModifier necklace)
                necklace.ChangeIncome(changes);
        }

        public void ChangeOutcome(UnitChanges changes)
        {
            if (Weapon is IChangesModifier weapon)
                weapon.ChangeOutcome(changes);

            if (Armor is IChangesModifier armor)
                armor.ChangeOutcome(changes);

            if (Necklace is IChangesModifier necklace)
                necklace.ChangeOutcome(changes);
        }
    }
}
