using System;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Items;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class EquipmentControl
    {
        public Equipment Equipment { get; }

        public EquipmentControl()
        {
            InitializeComponent();
        }

        public EquipmentControl(Equipment equipment): this()
        {
            Equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
            Equipment.Changed += Equipment_Changed;

            Unloaded += (_, _) => Equipment.Changed -= Equipment_Changed;

            Equipment_Changed(equipment, EquipmentType.Weapon, null, equipment.Weapon);
            Equipment_Changed(equipment, EquipmentType.Armor, null, equipment.Armor);
            Equipment_Changed(equipment, EquipmentType.Necklace, null, equipment.Necklace);
        }

        private void Equipment_Changed(Equipment arg1, EquipmentType equipmentType, IEquipmentItem oldItem, IEquipmentItem newItem)
        {
            switch (equipmentType)
            {
                case EquipmentType.Weapon:
                    _weapon.EquipmentItem = newItem;
                    break;

                case EquipmentType.Armor:
                    _armor.EquipmentItem = newItem;
                    break;

                case EquipmentType.Necklace:
                    _necklace.EquipmentItem = newItem;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
