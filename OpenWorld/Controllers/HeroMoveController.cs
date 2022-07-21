using System;
using System.Windows;
using System.Windows.Input;
using OpenWorld.Models;

namespace OpenWorld.Controllers
{
    internal class HeroMoveController: IDisposable
    {
        private readonly IInputElement _inputElement;
        private readonly Unit _hero;

        public HeroMoveController(IInputElement inputElement, Unit hero)
        {
            _inputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));

            _inputElement.MouseLeftButtonDown += InputElement_MouseLeftButtonDown;
            _inputElement.MouseMove += InputElement_MouseMove;
            _inputElement.MouseLeftButtonUp += InputElement_MouseLeftButtonUp;
        }

        private void InputElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Handled)
                return;

            if (_inputElement.CaptureMouse())
            {
                var mousePos = e.GetPosition(_inputElement);
                var x = (float)mousePos.X;
                var y = (float)mousePos.Y;
                _hero.MoveTarget.Set(x, y);
                var angle = _hero.Position.AngleTo(x, y);
                _hero.MoveDirection.Value = angle;

                _hero.MoveSpeed.SetMax();
                e.Handled = true;
            }
        }

        private void InputElement_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Handled)
                return;

            if (_inputElement.IsMouseCaptured)
            {
                var mousePos = e.GetPosition(_inputElement);
                var x = (float)mousePos.X;
                var y = (float)mousePos.Y;
                _hero.MoveTarget.Set(x, y);
                var angle = _hero.Position.AngleTo(x, y);
                _hero.MoveDirection.Value = angle;
                e.Handled = true;
            }
        }

        private void InputElement_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Handled)
                return;

            if (_inputElement.IsMouseCaptured)
            {
                _hero.MoveSpeed.SetMin();
                _inputElement.ReleaseMouseCapture();
                e.Handled = true;
            }
        }

        public void Dispose()
        {
            _inputElement.MouseLeftButtonDown -= InputElement_MouseLeftButtonDown;
            _inputElement.MouseMove -= InputElement_MouseMove;
            _inputElement.MouseLeftButtonUp -= InputElement_MouseLeftButtonUp;
        }
    }
}
