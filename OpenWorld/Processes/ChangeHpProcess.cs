using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Buffs;

namespace OpenWorld.Processes
{
    public class ChangeHpProcess : IProcess
    {
        private readonly Unit _fromUnit;
        private readonly Unit _targetUnit;
        private readonly float _delta;
        private readonly ushort _times;
        private readonly BuffType _buffType;
        private readonly IHasName _skillOrItemName;

        private readonly TimeLimiter _timeLimiter;
        private ushort _count;
        private Buff _buff;

        public ChangeHpProcess(Unit fromUnit, Unit targetUnit, float delta, TimeSpan interval, ushort times, BuffType buffType, IHasName skillOrItemName)
        {
            _fromUnit = fromUnit ?? throw new ArgumentNullException(nameof(fromUnit));
            _targetUnit = targetUnit ?? throw new ArgumentNullException(nameof(targetUnit));
            _delta = delta;
            _times = times;
            _buffType = buffType ?? throw new ArgumentNullException(nameof(buffType));
            _skillOrItemName = skillOrItemName;
            _timeLimiter = new TimeLimiter(interval);
        }

        public event Action<IProcess> Completed;

        public void Process(TimeSpan delta)
        {
            _timeLimiter.Do(() =>
            {
                if (_buff == null)
                    if (_targetUnit is IHasBuffs hasBuffs)
                    {
                        _buff = new Buff(_buffType);
                        hasBuffs.Add(_buff);
                    }

                Unit.Apply(_fromUnit, new UnitChanges(_delta, _skillOrItemName), _targetUnit);

                _count++;
                if (_count == _times)
                    Stop();
            });
        }

        public void Stop()
        {
            if (_buff != null)
                if (_targetUnit is IHasBuffs hasBuffs)
                    hasBuffs.Remove(_buff);

            Completed?.Invoke(this);
        }
    }
}
