using System;
using System.Windows;
using System.Windows.Input;
using Kalavarda.Primitives.Abstract;

namespace OpenWorld.Controllers
{
    public class MousePositionDetector: IDisposable, IMousePositionDetector
    {
        private static readonly float GameControlScale = (float)Settings.Default.GameControlScale;

        private readonly FrameworkElement _frameworkElement;
        private readonly IHasPosition _hero;
        private float x;
        private float y;

        public MousePositionDetector(FrameworkElement frameworkElement, IHasPosition hero)
        {
            _frameworkElement = frameworkElement ?? throw new ArgumentNullException(nameof(frameworkElement));
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));

            _frameworkElement.MouseMove += FrameworkElement_MouseMove;
        }

        private void FrameworkElement_MouseMove(object sender, MouseEventArgs e)
        {
            var w = _frameworkElement.ActualWidth / 2;
            var h = _frameworkElement.ActualHeight / 2;

            var mousePos = e.GetPosition(_frameworkElement);
            x = (float)(mousePos.X - w) / GameControlScale + _hero.Position.X;
            y = (float)(mousePos.Y - h) / GameControlScale + _hero.Position.Y;
        }

        public (float, float) GetPosition()
        {
            return (x, y);
        }

        public void Dispose()
        {
            _frameworkElement.MouseMove -= FrameworkElement_MouseMove;
        }
    }
}
