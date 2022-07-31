using System.Linq;
using System.Reflection;
using Kalavarda.Primitives.Units.Items;
using Kalavarda.Primitives.WPF;
using OpenWorld.Items;

namespace OpenWorld
{
    internal class ItemsRepository: IReadonlyItemsRepository
    {
        private const float JunkRatio = 1.18920f;
        private const float OrdinaryRatio = 1.4142f;
        private const float GoodRatio = 1.68179283f;
        private const float RareRatio = 2;

        private static readonly Assembly _resourcesAssembly = typeof(ItemsRepository).Assembly;

        public static readonly EquipmentItemType WoodSword_Junk = new ("Деревянный меч", ItemQuality.Junk, EquipmentType.Weapon)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png"),
            AttackRatio = JunkRatio
        };

        public static readonly EquipmentItemType WoodSword_Ordinary = new ("Деревянный меч", ItemQuality.Ordinary, EquipmentType.Weapon)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png"),
            AttackRatio = OrdinaryRatio
        };

        public static readonly EquipmentItemType WoodSword_Good = new("Деревянный меч", ItemQuality.Good, EquipmentType.Weapon)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png"),
            AttackRatio = GoodRatio
        };

        public static readonly EquipmentItemType WoodSword_Rare = new("Деревянный меч", ItemQuality.Rare, EquipmentType.Weapon)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png"),
            AttackRatio = RareRatio
        };

        public static readonly ItemType SpiderLegs_Junk = new("Паучьи лапки", ItemQuality.Junk)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/SpiderLegs.png")
        };

        public static readonly ItemType HpPotion_Junk = new("Снадобье исцеления", ItemQuality.Junk)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Alchemy/HpPotion.png")
        };

        public static readonly EquipmentItemType Armor_Junk = new("Куртка из дешёвой кожи", ItemQuality.Junk, EquipmentType.Armor)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Armor.png"),
            DefRatio = JunkRatio
        };

        public static readonly EquipmentItemType Armor_Ordinary = new("Куртка из дешёвой кожи", ItemQuality.Ordinary, EquipmentType.Armor)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Armor.png"),
            DefRatio = OrdinaryRatio
        };

        public static readonly EquipmentItemType Armor_Good = new("Куртка из дешёвой кожи", ItemQuality.Good, EquipmentType.Armor)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Armor.png"),
            DefRatio = GoodRatio
        };

        public static readonly EquipmentItemType Armor_Rare = new("Куртка из дешёвой кожи", ItemQuality.Rare, EquipmentType.Armor)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Armor.png"),
            DefRatio = RareRatio
        };

        public static readonly EquipmentItemType Necklace_Junk = new("Дешёвое ожерелье", ItemQuality.Junk, EquipmentType.Necklace)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Necklace.png"),
            DefRatio = JunkRatio
        };

        public static readonly EquipmentItemType Necklace_Ordinary = new("Дешёвое ожерелье", ItemQuality.Ordinary, EquipmentType.Necklace)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Necklace.png"),
            DefRatio = OrdinaryRatio
        };

        public static readonly EquipmentItemType Necklace_Good = new("Дешёвое ожерелье", ItemQuality.Good, EquipmentType.Necklace)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Necklace.png"),
            DefRatio = GoodRatio
        };

        public static readonly EquipmentItemType Necklace_Rare = new("Дешёвое ожерелье", ItemQuality.Rare, EquipmentType.Necklace)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Necklace.png"),
            DefRatio = RareRatio
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
            Armor_Good,
            Armor_Rare,
            Necklace_Junk,
            Necklace_Ordinary,
            Necklace_Good,
            Necklace_Rare
        };

        public ItemType GetById(uint id)
        {
            return _types.FirstOrDefault(t => t.Id == id);
        }
    }
}
