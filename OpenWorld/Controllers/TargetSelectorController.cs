using System;
using System.Windows;
using System.Windows.Input;
using Kalavarda.Primitives.Units;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class TargetSelectorController: IDisposable
    {
        private readonly IInputElement _inputElement;
        private readonly Hero _hero;
        private readonly ITargetSelector _targetSelector;

        public TargetSelectorController(IInputElement inputElement, Hero hero, ITargetSelector targetSelector)
        {
            _inputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _targetSelector = targetSelector ?? throw new ArgumentNullException(nameof(targetSelector));

            _inputElement.KeyDown += InputElement_KeyDown;
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

                        if (_hero.Target != null)
                            _hero.Target.IsSelected = false;

                        _hero.Target = newTarget;
                        newTarget.IsSelected = true;

                        e.Handled = true;
                    }
                    break;

                case Key.F1:
                    if (_hero.Target != null)
                        _hero.Target.IsSelected = false;

                    _hero.Target = _hero;

                    e.Handled = true;
                    break;

                case Key.Escape:
                    if (_hero.Target != null)
                    {
                        _hero.Target.IsSelected = false;
                        _hero.Target = null;
                        e.Handled = true;
                    }
                    break;
            }
        }

        public void Dispose()
        {
            _inputElement.KeyDown -= InputElement_KeyDown;
        }
    }
}
