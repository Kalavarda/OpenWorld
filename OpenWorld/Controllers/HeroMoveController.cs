using System;
using System.Windows;
using System.Windows.Input;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Controllers
{
    internal class HeroMoveController: IDisposable
    {
        private readonly FrameworkElement _inputElement;
        private readonly Unit _hero;
        private readonly IMousePositionDetector _mousePositionDetector;

        public HeroMoveController(FrameworkElement inputElement, Unit hero, IMousePositionDetector mousePositionDetector)
        {
            _inputElement = inputElement ?? throw new ArgumentNullException(nameof(inputElement));
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _mousePositionDetector = mousePositionDetector ?? throw new ArgumentNullException(nameof(mousePositionDetector));

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
            if (_hero.IsDead)
                return;

            var (x, y) = _mousePositionDetector.GetPosition();
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
