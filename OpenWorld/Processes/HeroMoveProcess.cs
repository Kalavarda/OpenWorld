using System;
using System.Collections.Generic;
using System.Linq;
using Kalavarda.Primitives.Process;
using OpenWorld.Models;

namespace OpenWorld.Processes
{
    public class HeroMoveProcess : IProcess, IIncompatibleProcess
    {
        private const float MinDistance = 0.1f;

        private readonly Hero _hero;
        public event Action<IProcess> Completed;

        public HeroMoveProcess(Hero hero)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
        }

        public void Process(TimeSpan delta)
        {
            if (_hero.Position.DistanceTo(_hero.MoveTarget) < MinDistance)
                return;

            var speed = _hero.MoveSpeed.Value;
            var dt = (float)delta.TotalSeconds;
            var x = _hero.Position.X + dt * speed * MathF.Cos(_hero.MoveDirection.Value);
            var y = _hero.Position.Y + dt * speed * MathF.Sin(_hero.MoveDirection.Value);

            _hero.Position.Set(x, y);
        }

        public void Stop()
        {
            Completed?.Invoke(this);
        }

        public IReadOnlyCollection<IProcess> GetIncompatibleProcesses(IReadOnlyCollection<IProcess> processes)
        {
            return processes.OfType<HeroMoveProcess>().ToArray();
        }
    }
}
