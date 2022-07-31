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
        private readonly Hero _hero;
        private readonly ICreatureEvents _eventAggregator;
        private readonly ILevelMultiplier _levelMultiplier;
        private readonly IRandom _random;

        private void Award(Mob mob)
        {
            _hero.XP.Value += _levelMultiplier.GetValue(1, mob.Level);

            if (_random.Chance(2))
            {
                var count = (uint)_random.Int(0, (int)_levelMultiplier.GetValue(2, mob.Level));
                var chitin = new Item(ItemsRepository.SpiderLegs_Junk) { Count = count };
                _hero.Bag.Add(chitin);

                if (_random.Chance(2))
                {
                    if (_random.Chance(2))
                    {
                        if (_random.Chance(2))
                        {
                            _hero.Bag.Add(new EquipmentItem(ItemsRepository.WoodSword_Good));
                        }
                        else
                            _hero.Bag.Add(new EquipmentItem(ItemsRepository.WoodSword_Ordinary));
                    }
                    else
                        _hero.Bag.Add(new EquipmentItem(ItemsRepository.WoodSword_Junk));
                }

                if (_random.Chance(2))
                {
                    if (_random.Chance(2))
                    {
                        if (_random.Chance(2))
                        {
                            _hero.Bag.Add(new EquipmentItem(ItemsRepository.Armor_Good));
                        }
                        else
                            _hero.Bag.Add(new EquipmentItem(ItemsRepository.Armor_Ordinary));
                    }
                    else
                        _hero.Bag.Add(new EquipmentItem(ItemsRepository.Armor_Junk));
                }
            }
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
