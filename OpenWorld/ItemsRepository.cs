using System.Linq;
using System.Reflection;
using Kalavarda.Primitives.Units.Items;
using Kalavarda.Primitives.WPF;
using OpenWorld.Items;

namespace OpenWorld
{
    internal class ItemsRepository: IReadonlyItemsRepository
    {
        private static readonly Assembly _resourcesAssembly = typeof(ItemsRepository).Assembly;

        public static readonly EquipmentItemType WoodSword_Junk = new (1, "Деревянный меч", ItemQuality.Junk, EquipmentType.Weapon)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png"),
            AttackRatio = 1.18920f
        };

        public static readonly EquipmentItemType WoodSword_Ordinary = new (2, "Деревянный меч", ItemQuality.Ordinary, EquipmentType.Weapon)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png"),
            AttackRatio = 1.4142f
        };

        public static readonly EquipmentItemType WoodSword_Good = new (3, "Деревянный меч", ItemQuality.Good, EquipmentType.Weapon)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png"),
            AttackRatio = 1.68179283f
        };

        public static readonly ItemType SpiderLegs_Junk = new(4, "Паучьи лапки", ItemQuality.Junk)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/SpiderLegs.png")
        };

        public static readonly ItemType HpPotion_Junk = new(5, "Снадобье исцеления", ItemQuality.Junk)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Alchemy/HpPotion.png")
        };

        public static readonly EquipmentItemType Armor_Junk = new(6, "Куртка", ItemQuality.Junk, EquipmentType.Armor)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Armor.png"),
            DefRatio = 1.18920f
        };

        public static readonly EquipmentItemType Armor_Ordinary = new(7, "Куртка", ItemQuality.Ordinary, EquipmentType.Armor)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Armor.png"),
            DefRatio = 1.4142f
        };

        public static readonly EquipmentItemType Armor_Good = new(8, "Куртка", ItemQuality.Good, EquipmentType.Armor)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Armor.png"),
            DefRatio = 1.68179283f
        };

        private static readonly ItemType[] _types =
        {
            WoodSword_Junk,
            WoodSword_Ordinary,
            WoodSword_Good,
            SpiderLegs_Junk,
            HpPotion_Junk,
            Armor_Junk,
            Armor_Ordinary,
            Armor_Good
        };

        public ItemType GetById(uint id)
        {
            return _types.FirstOrDefault(t => t.Id == id);
        }
    }
}
