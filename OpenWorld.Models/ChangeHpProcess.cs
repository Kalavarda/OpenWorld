using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Models
{
    public class ChangeHpProcess : IProcess
    {
        private readonly Unit _fromUnit;
        private readonly Unit _targetUnit;
        private readonly float _delta;
        private readonly ushort _times;

        private readonly TimeLimiter _timeLimiter;
        private ushort _count;

        public ChangeHpProcess(Unit fromUnit, Unit targetUnit, float delta, TimeSpan interval, ushort times)
        {
            _fromUnit = fromUnit ?? throw new ArgumentNullException(nameof(fromUnit));
            _targetUnit = targetUnit ?? throw new ArgumentNullException(nameof(targetUnit));
            _delta = delta;
            _times = times;
            _timeLimiter = new TimeLimiter(interval);
        }

        public event Action<IProcess> Completed;

        public void Process(TimeSpan delta)
        {
            _timeLimiter.Do(() =>
            {
                Unit.Apply(_fromUnit, new UnitChanges(_delta), _targetUnit);

                _count++;
                if (_count == _times)
                    Stop();
            });
        }

        public void Stop()
        {
            Completed?.Invoke(this);
        }
    }
}
