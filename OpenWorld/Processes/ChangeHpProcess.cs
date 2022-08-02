using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Buffs;
using Kalavarda.Primitives.Units.Interfaces;

namespace OpenWorld.Processes
{
    public class ChangeHpProcess : IProcess
    {
        private readonly UnitPhantom _fromUnit;
        private readonly IFighter _targetUnit;
        private readonly ushort _times;
        private readonly BuffType _buffType;
        private readonly IHasName _skillOrItemName;

        private readonly TimeLimiter _timeLimiter;
        private ushort _count;
        private Buff _buff;
        private readonly UnitChanges _unitChanges;

        public ChangeHpProcess(IFighter from, IFighter target, float hpDelta, TimeSpan interval, ushort times, BuffType buffType, IHasName skillOrItemName)
        {
            if (from is UnitPhantom phantom)
                _fromUnit = phantom;
            else if (from is Unit unit)
                _fromUnit = new UnitPhantom(unit);
            else
                throw new NotImplementedException();

            _targetUnit = target ?? throw new ArgumentNullException(nameof(target));
            _times = times;
            _buffType = buffType ?? throw new ArgumentNullException(nameof(buffType));
            _skillOrItemName = skillOrItemName;
            _timeLimiter = new TimeLimiter(interval);
            
            _unitChanges = new UnitChanges(hpDelta, _skillOrItemName);
            if (from is IChangesModifier modifier1)
                modifier1.ChangeOutcome(_unitChanges);
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

                Unit.Apply(_fromUnit, _unitChanges, _targetUnit);

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
