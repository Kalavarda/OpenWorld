using System;
using System.Linq;
using Kalavarda.Primitives.Process;
using OpenWorld.Models;

namespace OpenWorld.Processes
{
    internal class MobsProcess: IProcess
    {
        private readonly Hero _hero;
        private readonly IProcessor _processor;

        public event Action<IProcess> Completed;

        public MobsProcess(Hero hero, IProcessor processor)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _processor = processor ?? throw new ArgumentNullException(nameof(processor));
        }

        public void Process(TimeSpan delta)
        {
            RemoveDeadMobs();

            foreach (var mob in Mob.Mobs)
                switch (mob.State)
                {
                    case Mob.MobState.New:
                        if (DateTime.Now - mob.StateChangedTime > Settings.Default.MobNewDuration)
                            mob.State = Mob.MobState.Idle;
                        break;

                    case Mob.MobState.Idle:
                        if (mob.Position.DistanceTo(_hero.Position) < mob.AggrDistance)
                        {
                            mob.State = Mob.MobState.Fight;
                            mob.Target = _hero;
                            _processor.Add(mob.CreateFightProcess(_processor));
                        }

                        break;

                    case Mob.MobState.Fight:
                        if (mob.IsDead)
                            mob.State = Mob.MobState.Dead;

                        if (mob.Position.DistanceTo(mob.Spawn.Position) > mob.MaxDistanceFromSpawn)
                            mob.State = Mob.MobState.Returning;

                        if (mob.Target.HP.IsMin)
                            mob.State = Mob.MobState.Returning;

                        break;

                    case Mob.MobState.Returning:
                        mob.Target = null;

                        if (mob.Position.DistanceTo(mob.Spawn.Position) < mob.Spawn.Radius)
                            mob.State = Mob.MobState.Idle;
                        else
                            MoveToSpawn(mob, delta);
                        break;

                    case Mob.MobState.Dead:
                        break;
                }
        }

        private static void MoveToSpawn(Mob mob, TimeSpan delta)
        {
            var angle = mob.Position.AngleTo(mob.Spawn.Position);
            var d = Settings.Default.MobReturnSpeedRatio * mob.MoveSpeed.Max * (float)delta.TotalSeconds;
            var dx = d * MathF.Cos(angle);
            var dy = d * MathF.Sin(angle);
            mob.Position.Set(mob.Position.X + dx, mob.Position.Y + dy);
        }

        private static void RemoveDeadMobs()
        {
            var deadMobs = Mob.Mobs
                .Where(m => m.State == Mob.MobState.Dead)
                .Where(m => DateTime.Now - m.StateChangedTime > Settings.Default.MobDeadDuration);
            foreach (var mob in deadMobs)
                Mob.Remove(mob);
        }

        public void Stop()
        {
            Completed?.Invoke(this);
        }
    }
}
