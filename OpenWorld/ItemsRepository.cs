using System.Linq;
using Kalavarda.Primitives.Units.Items;
using Kalavarda.Primitives.WPF;
using OpenWorld.Controls;

namespace OpenWorld
{
    internal class ItemsRepository: IItemsRepository
    {
        public static readonly ItemType WoodSword_Junk = new(1, "Деревянный меч", ItemQuality.Junk)
        {
            ImageUri = typeof(HeroControl).Assembly.GetResourceUri("Images/Items/Sword.png")
        };

        public static readonly ItemType WoodSword_Ordinary = new(2, "Деревянный меч", ItemQuality.Ordinary)
        {
            ImageUri = typeof(HeroControl).Assembly.GetResourceUri("Images/Items/Sword.png")
        };

        public static readonly ItemType WoodSword_Good = new(3, "Деревянный меч", ItemQuality.Good)
        {
            ImageUri = typeof(HeroControl).Assembly.GetResourceUri("Images/Items/Sword.png")
        };

        public static readonly ItemType Chitin = new(4, "Хитин", ItemQuality.Junk)
        {
            ImageUri = typeof(HeroControl).Assembly.GetResourceUri("Images/Items/Chitin.jpg")
        };

        private static readonly ItemType[] _types =
        {
            WoodSword_Junk,
            WoodSword_Ordinary,
            WoodSword_Good,
            Chitin
        };

        public ItemType GetById(uint id)
        {
            return _types.FirstOrDefault(t => t.Id == id);
        }
    }
}
