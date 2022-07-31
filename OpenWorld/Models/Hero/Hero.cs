using System;
using System.Collections.Generic;
using System.Linq;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Items;
using OpenWorld.Models.Hero.Skills;

namespace OpenWorld.Models.Hero
{
    public class Hero: Unit, IHasLevel, IChangesModifier
    {
        private readonly ISkill[] _skills;
        private ushort _level = 1;

        public Hero(RangeF moveSpeed): base(moveSpeed)
        {
            Bounds = new RoundBounds(Position, 0.75f / 2);

            HP.Max = 10;
            HP.SetMax();

            _skills = new ISkill[]
            {
                new SimpleStrike()
            };
            foreach (var skill in _skills.OfType<IMakeSounds>())
                skill.PlaySound += RaisePlaySound;
        }

        public override BoundsF Bounds { get; }

        public override IEnumerable<ISkill> Skills => _skills;

        public ushort Level
        {
            get => _level;
            set
            {
                if (_level == value)
                    return;

                _level = value;
                LevelChanged?.Invoke(this);
            }
        }

        public event Action<IHasLevel> LevelChanged;

        public RangeF XP { get; } = new();

        public IItemContainer Bag { get; } = new ItemContainer();

        public Equipment Equipment { get; } = new();

        public void Use(IEquipmentItem equipmentItem)
        {
            var eqItem = (EquipmentItem)equipmentItem;
            switch (eqItem.EquipmentType)
            {
                case EquipmentType.Weapon:
                    Equipment.Weapon = equipmentItem;
                    break;

                case EquipmentType.Armor:
                    Equipment.Armor = equipmentItem;
                    break;

                case EquipmentType.Necklace:
                    Equipment.Necklace = equipmentItem;
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        public void ChangeIncome(UnitChanges changes)
        {
            Equipment.ChangeIncome(changes);
        }

        public void ChangeOutcome(UnitChanges changes)
        {
            Equipment.ChangeOutcome(changes);
        }
    }
}
