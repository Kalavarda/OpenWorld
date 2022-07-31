using Kalavarda.Primitives.Units.Items;

namespace OpenWorld.Items;

internal class EquipmentItemType : ItemType, IModifierParameters
{
    public EquipmentType EquipmentType { get; }

    public EquipmentItemType(string name, ItemQuality quality, EquipmentType equipmentType) : base(name, quality)
    {
        EquipmentType = equipmentType;
    }

    public float? AttackRatio { get; set; }

    public float? DefRatio { get; set; }
}