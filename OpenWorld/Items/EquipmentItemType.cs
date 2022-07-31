using Kalavarda.Primitives.Units.Items;

namespace OpenWorld.Items;

internal class EquipmentItemType : ItemType, IModifierParameters
{
    public EquipmentType EquipmentType { get; }

    public EquipmentItemType(uint id, string name, ItemQuality quality, EquipmentType equipmentType) : base(id, name, quality)
    {
        EquipmentType = equipmentType;
    }

    public float? AttackRatio { get; set; }

    public float? DefRatio { get; set; }
}