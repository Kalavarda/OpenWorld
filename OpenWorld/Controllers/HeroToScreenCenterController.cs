using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.WPF;

namespace OpenWorld.Controllers
{
    internal class HeroToScreenCenterController: IDisposable
    {
        private readonly FrameworkElement _container;
        private readonly IHasPosition _hero;
        private readonly ScaleTransform _scaleTransform;
        private readonly TranslateTransform _translateTransform;

        public HeroToScreenCenterController(FrameworkElement container, IHasPosition hero)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _hero = hero;

            var tg = (TransformGroup)_container.RenderTransform;
            _scaleTransform = tg.Children.OfType<ScaleTransform>().First();
            _translateTransform = tg.Children.OfType<TranslateTransform>().First();

            _container.SizeChanged += _container_SizeChanged;
            _hero.Position.Changed += Position_Changed;
            Position_Changed(_hero.Position);
        }

        private void _container_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Position_Changed(_hero.Position);
        }

        private void Position_Changed(Kalavarda.Primitives.Geometry.PointF heroPosition)
        {
            if (_container.ActualWidth > 0 && _container.ActualHeight > 0)
                _container.Do(() =>
                {
                    _translateTransform.X = -heroPosition.X * _scaleTransform.ScaleX + _container.ActualWidth / 2;
                    _translateTransform.Y = -heroPosition.Y * _scaleTransform.ScaleY + _container.ActualHeight / 2;
                });
        }

        public void Dispose()
        {
            _hero.Position.Changed -= Position_Changed;
            _container.SizeChanged -= _container_SizeChanged;
        }
    }
}
