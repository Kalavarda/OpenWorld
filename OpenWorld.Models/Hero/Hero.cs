using System;
using System.Collections.Generic;
using System.Linq;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Hero
{
    public class Hero: Unit, IHasLevel
    {
        private readonly ISkill[] _skills;
        private ushort _level = 1;

        public Hero(RangeF moveSpeed): base(moveSpeed)
        {
            Bounds = new RoundBounds(Position, 0.75f / 2);

            HP.Max = 10_000;
            HP.SetMax();

            _skills = new ISkill[]
            {
                new SimpleStrike(this)
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
    }
}
