using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Items;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    public class LootController: IDisposable
    {
        private const int LuckChance = 10;

        private readonly Hero _hero;
        private readonly ICreatureEvents _eventAggregator;
        private readonly ILevelMultiplier _levelMultiplier;
        private readonly IRandom _random;

        private void Award(Mob mob)
        {
            _hero.XP.Value += _levelMultiplier.GetValue(1, mob.Level);

            var count = (uint)_random.Int(0, (int)_levelMultiplier.GetValue(5, mob.Level));
            var chitin = new Item(ItemsRepository.SpiderLegs_Junk) { Count = count };
            _hero.Bag.Add(chitin);

            if (_random.Chance(LuckChance))
                switch (_random.Int(1, 3))
                {
                    case 1:
                        _hero.Bag.Add(new EquipmentItem(GetWeaponType()));
                        break;
                    
                    case 2:
                        _hero.Bag.Add(new EquipmentItem(GetArmorType()));
                        break;

                    case 3:
                        _hero.Bag.Add(new EquipmentItem(GetNecklaceType()));
                        break;
                }
        }

        private EquipmentItemType GetWeaponType()
        {
            switch (CreateQuality())
            {
                case ItemQuality.Junk:
                    return ItemsRepository.WoodSword_Junk;

                case ItemQuality.Ordinary:
                    return ItemsRepository.WoodSword_Ordinary;

                case ItemQuality.Good:
                    return ItemsRepository.WoodSword_Good;

                case ItemQuality.Rare:
                    return ItemsRepository.WoodSword_Rare;

                default:
                    throw new NotImplementedException();
            }
        }

        private EquipmentItemType GetArmorType()
        {
            switch (CreateQuality())
            {
                case ItemQuality.Junk:
                    return ItemsRepository.Armor_Junk;

                case ItemQuality.Ordinary:
                    return ItemsRepository.Armor_Ordinary;

                case ItemQuality.Good:
                    return ItemsRepository.Armor_Good;

                case ItemQuality.Rare:
                    return ItemsRepository.Armor_Rare;

                default:
                    throw new NotImplementedException();
            }
        }

        private EquipmentItemType GetNecklaceType()
        {
            switch (CreateQuality())
            {
                case ItemQuality.Junk:
                    return ItemsRepository.Necklace_Junk;

                case ItemQuality.Ordinary:
                    return ItemsRepository.Necklace_Ordinary;

                case ItemQuality.Good:
                    return ItemsRepository.Necklace_Good;

                case ItemQuality.Rare:
                    return ItemsRepository.Necklace_Rare;

                default:
                    throw new NotImplementedException();
            }
        }

        private ItemQuality CreateQuality()
        {
            if (_random.Chance(LuckChance))
                return ItemQuality.Junk;

            if (_random.Chance(LuckChance))
            {
                if (_random.Chance(LuckChance))
                {
                    if (_random.Chance(LuckChance))
                    {
                        if (_random.Chance(LuckChance))
                            return ItemQuality.Epic;
                        else
                            return ItemQuality.Legendary;
                    }
                    else
                        return ItemQuality.Rare;
                }
                else
                    return ItemQuality.Good;
            }

            return ItemQuality.Ordinary;
        }

        public LootController(Hero hero, ICreatureEvents eventAggregator, ILevelMultiplier levelMultiplier, IRandom random)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _levelMultiplier = levelMultiplier ?? throw new ArgumentNullException(nameof(levelMultiplier));
            _random = random ?? throw new ArgumentNullException(nameof(random));

            _eventAggregator.Died += Mob_Died;
        }

        private void Mob_Died(ICreature obj)
        {
            if (obj is Mob mob)
                Award(mob);
        }

        public void Dispose()
        {
            _eventAggregator.Died -= Mob_Died;
        }
    }
}
