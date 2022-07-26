using System;
using Kalavarda.Primitives.Units.Items;
using Kalavarda.Primitives.WPF;
using OpenWorld.Controls;

namespace OpenWorld
{
    internal class ItemsRepository: IItemsRepository
    {
        public static readonly ItemType Sword = new(1, "Меч-кладенец", ItemQuality.Ordinary)
        {
            ImageUri = typeof(HeroControl).Assembly.GetResourceUri("Images/Items/Sword.png")
        };

        public static readonly ItemType Chitin = new(2, "Хитин", ItemQuality.Junk)
        {
            ImageUri = typeof(HeroControl).Assembly.GetResourceUri("Images/Items/Chitin.jpg")
        };

        public ItemType GetById(uint id)
        {
            switch (id)
            {
                case 1:
                    return Sword;

                case 2:
                    return Chitin;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
