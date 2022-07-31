using System.Linq;
using System.Reflection;
using Kalavarda.Primitives.Units.Items;
using Kalavarda.Primitives.WPF;
using OpenWorld.Controls;

namespace OpenWorld
{
    internal class ItemsRepository: IReadonlyItemsRepository
    {
        private static readonly Assembly _resourcesAssembly = typeof(ItemsRepository).Assembly;

        public static readonly ItemType WoodSword_Junk = new(1, "Деревянный меч", ItemQuality.Junk)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png")
        };

        public static readonly ItemType WoodSword_Ordinary = new(2, "Деревянный меч", ItemQuality.Ordinary)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png")
        };

        public static readonly ItemType WoodSword_Good = new(3, "Деревянный меч", ItemQuality.Good)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Sword.png")
        };

        public static readonly ItemType SpiderLegs_Junk = new(4, "Паучьи лапки", ItemQuality.Junk)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/SpiderLegs.png")
        };

        public static readonly ItemType HpPotion_Junk = new(5, "Снадобье исцеления", ItemQuality.Junk)
        {
            ImageUri = _resourcesAssembly.GetResourceUri("Resources/Items/Alchemy/HpPotion.png")
        };

        private static readonly ItemType[] _types =
        {
            WoodSword_Junk,
            WoodSword_Ordinary,
            WoodSword_Good,
            SpiderLegs_Junk,
            HpPotion_Junk
        };

        public ItemType GetById(uint id)
        {
            return _types.FirstOrDefault(t => t.Id == id);
        }
    }
}
