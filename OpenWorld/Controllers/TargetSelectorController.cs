using System;
using System.Windows;
using System.Windows.Input;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Fight;
using Kalavarda.Primitives.Units.Interfaces;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class TargetSelectorController: IDisposable
    {
        private readonly IInputElement _inputElement;
        private readonly Hero _hero;
        private readonly ITargetSelector _targetSelector;
        private readonly ICreatureEvents _creatureAggregator;
        private readonly IFightController _fightController;
        private static readonly float TargetMaxDistance = Settings.Default.TargetMaxDistance;

        public TargetSelectorController(IInputElement inputElement, Hero hero, ITargetSelector targetSelector, ICreatureEvents creatureAggregator, IFightController fightController)
        {
            _inputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _targetSelector = targetSelector ?? throw new ArgumentNullException(nameof(targetSelector));
            _creatureAggregator = creatureAggregator ?? throw new ArgumentNullException(nameof(creatureAggregator));
            _fightController = fightController ?? throw new ArgumentNullException(nameof(fightController));

            _inputElement.KeyDown += InputElement_KeyDown;
            _creatureAggregator.Died += Mob_Died;
            _hero.NegativeSkillReceived += Hero_NegativeSkillReceived;
            _hero.Position.Changed += HeroPosition_Changed;
        }

        private void HeroPosition_Changed(PointF pos)
        {
            if (_hero.Target != null)
            {
                var distance = _hero.Position.DistanceTo(_hero.Target.Position);
                if (distance > TargetMaxDistance)
                    Select(null);
            }
        }

        private void Hero_NegativeSkillReceived(Unit from, Unit to)
        {
            if (_hero.Target == null)
                Select(from);
        }

        private void Mob_Died(ICreature creature)
        {
            if (_hero.Target == creature)
            {
                var newTarget = _fightController.CurrentFight != null
                    ? _targetSelector.Select(true)
                    : null;
                Select(newTarget);
            }
        }

        private void InputElement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Handled)
                return;

            switch (e.Key)
            {
                case Key.Tab:
                    var newTarget = _targetSelector.Select();
                    if (newTarget != null)
                    {
                        Select(newTarget);
                        newTarget.IsSelected = true;

                        e.Handled = true;
                    }
                    break;

                case Key.F1:
                    Select(_hero);

                    e.Handled = true;
                    break;

                case Key.Escape:
                    if (_hero.Target != null)
                    {
                        Select(null);
                        e.Handled = true;
                    }
                    break;
            }
        }

        private void Select(Unit newTarget)
        {
            if (_hero.Target != null)
                _hero.Target.IsSelected = false;

            _hero.Target = newTarget;

            if (newTarget != null)
                newTarget.IsSelected = true;
        }

        public void Dispose()
        {
            _inputElement.KeyDown -= InputElement_KeyDown;
            _creatureAggregator.Died -= Mob_Died;
            _hero.NegativeSkillReceived -= Hero_NegativeSkillReceived;
            _hero.Position.Changed -= HeroPosition_Changed;
        }
    }
}
