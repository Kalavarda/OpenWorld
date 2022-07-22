using System;
using System.Windows;
using System.Windows.Input;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Controllers
{
    internal class HeroMoveController: IDisposable
    {
        private readonly FrameworkElement _inputElement;
        private readonly Unit _hero;
        private static readonly float GameControlScale = (float)Settings.Default.GameControlScale;

        public HeroMoveController(FrameworkElement inputElement, Unit hero)
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
                Process(e);
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
                Process(e);
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

        private void Process(MouseEventArgs e)
        {
            var w = _inputElement.ActualWidth / 2;
            var h = _inputElement.ActualHeight / 2;

            var mousePos = e.GetPosition(_inputElement);
            var x = (float)(mousePos.X - w) * GameControlScale;
            var y = (float)(mousePos.Y - h) * GameControlScale;
            _hero.MoveTarget.Set(x, y);
            var angle = _hero.Position.AngleTo(x, y);
            _hero.MoveDirection.Value = angle;
        }

        public void Dispose()
        {
            _inputElement.MouseLeftButtonDown -= InputElement_MouseLeftButtonDown;
            _inputElement.MouseMove -= InputElement_MouseMove;
            _inputElement.MouseLeftButtonUp -= InputElement_MouseLeftButtonUp;
        }
    }
}
