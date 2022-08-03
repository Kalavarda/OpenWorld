using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Units.Fight;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.WPF.Binds;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class TargetSelectorController: IDisposable
    {
        private readonly Hero _hero;
        private readonly ITargetSelector _targetSelector;
        private readonly ISelectableEvents _creatureAggregator;
        private readonly IFightController _fightController;
        private readonly IKeyBindsController _keyBindsController;
        private static readonly float TargetMaxDistance = Settings.Default.TargetMaxDistance;

        public TargetSelectorController(Hero hero, ITargetSelector targetSelector, ISelectableEvents creatureAggregator, IFightController fightController, IKeyBindsController keyBindsController)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _targetSelector = targetSelector ?? throw new ArgumentNullException(nameof(targetSelector));
            _creatureAggregator = creatureAggregator ?? throw new ArgumentNullException(nameof(creatureAggregator));
            _fightController = fightController ?? throw new ArgumentNullException(nameof(fightController));
            _keyBindsController = keyBindsController ?? throw new ArgumentNullException(nameof(keyBindsController));

            _creatureAggregator.IsSelectableChanged += Mob_IsSelectableChanged;
            _hero.NegativeSkillReceived += Hero_NegativeSkillReceived;
            _hero.Position.Changed += HeroPosition_Changed;
            _keyBindsController.BindActivated += KeyBindsController_BindActivated;
        }

        private void KeyBindsController_BindActivated(KeyBind bind)
        {
            switch (bind.Code)
            {
                case KeyBinds.Code_SelectTarget:
                    var newTarget = _targetSelector.Select();
                    if (newTarget != null)
                        Select(newTarget);
                    break;

                case KeyBinds.Code_SelectSelf:
                    Select(_hero);
                    break;

                case KeyBinds.Code_SelectNone:
                    Select(null);
                    break;
            }
        }

        private void HeroPosition_Changed(PointF pos)
        {
            if (_hero.Target is IHasPosition hasPosition)
            {
                var distance = _hero.Position.DistanceTo(hasPosition.Position);
                if (distance > TargetMaxDistance)
                    Select(null);
            }
        }

        private void Hero_NegativeSkillReceived(IFighter from, IFighter to)
        {
            if (_hero.Target == null)
                if (from is ISelectable { IsSelectable: true } selectable)
                    Select(selectable);
        }

        private void Mob_IsSelectableChanged(ISelectable selectable)
        {
            if (!selectable.IsSelectable)
                if (_hero.Target == selectable)
                {
                    var newTarget = _fightController.CurrentFight != null
                        ? _targetSelector.Select(true)
                        : null;
                    Select(newTarget);
                }
        }

        private void Select(ISelectable newTarget)
        {
            _hero.Target = newTarget;
        }

        public void Dispose()
        {
            _creatureAggregator.IsSelectableChanged -= Mob_IsSelectableChanged;
            _hero.NegativeSkillReceived -= Hero_NegativeSkillReceived;
            _hero.Position.Changed -= HeroPosition_Changed;
            _keyBindsController.BindActivated -= KeyBindsController_BindActivated;
        }
    }
}
