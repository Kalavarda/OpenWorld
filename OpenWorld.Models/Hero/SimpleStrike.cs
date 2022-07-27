﻿using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models.Hero
{
    public class SimpleStrike: ISkill, IDistanceSkill, IMakeSounds
    {
        private const int AttackPower = 1;

        private readonly TimeLimiter _timeLimiter = new(TimeSpan.FromSeconds(1));

        public string Name => "Простой удар";

        public ITimeLimiter TimeLimiter => _timeLimiter;

        public IProcess Use(ISkilled actor)
        {
            if (actor is Unit unit)
                _timeLimiter.Do(() =>
                {
                    var distance = unit.Position.DistanceTo(unit.Target.Position);
                    if (distance > MaxDistance)
                        return;

                    var changes = new UnitChanges(-AttackPower);
                    Unit.Apply(unit, changes, unit.Target);

                    PlaySound?.Invoke(nameof(Hero) + nameof(SimpleStrike));
                });

            return null;
        }

        public float MinDistance => 0;

        public float MaxDistance => 0.5f;

        public event Action<string> PlaySound;

        public string Key => nameof(SimpleStrike);
    }
}
