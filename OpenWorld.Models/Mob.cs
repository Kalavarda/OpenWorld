﻿using System;
using System.Collections.Generic;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;

namespace OpenWorld.Models
{
    public abstract class Mob: Unit
    {
        private static readonly ICollection<Mob> _mobs = new List<Mob>();
        private MobState _state = MobState.New;

        public static IEnumerable<Mob> Mobs => _mobs;

        public MobState State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;
                StateChangedTime = DateTime.Now;
            }
        }

        public DateTime StateChangedTime { get; private set; } = DateTime.Now;
        
        public abstract float AggrDistance { get; }
        
        public abstract float MaxDistanceFromSpawn { get; }

        public abstract IProcess CreateFightProcess(IProcessor processor);

        public Spawn Spawn { get; }

        protected Mob(RangeF moveSpeed, Spawn spawn) : base(moveSpeed)
        {
            _mobs.Add(this);
            Spawn = spawn;
        }

        public enum MobState
        {
            New,
            Idle,
            Fight,
            Dead,
            Returning
        }

        public static void Remove(Mob mob)
        {
            _mobs.Remove(mob);
        }
    }
}
