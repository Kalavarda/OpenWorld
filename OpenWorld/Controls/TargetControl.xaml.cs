using System.Windows;
using System.Windows.Controls;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Geometry;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.WPF;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class TargetControl
    {
        private Hero _hero;

        public Hero Hero
        {
            get => _hero;
            set
            {
                if (_hero == value)
                    return;

                if (_hero != null)
                    _hero.TargetChanged -= Hero_TargetChanged;

                _hero = value;

                if (_hero != null)
                {
                    _hero.TargetChanged += Hero_TargetChanged;
                    Hero_TargetChanged(null, _hero.Target);
                }
            }
        }

        private void Hero_TargetChanged(ISelectable oldTarget, ISelectable newTarget)
        {
            if (oldTarget is IHasPosition hasPosition1)
                hasPosition1.Position.Changed -= Position_Changed;

            this.Do(() =>
            {
                if (newTarget == null)
                {
                    Visibility = Visibility.Collapsed;
                    return;
                }

                if (newTarget is IHasBounds hasBounds)
                {
                    Width = hasBounds.Bounds.Width * 1.23;
                    Height = hasBounds.Bounds.Height * 1.23;
                    _border.BorderThickness = new Thickness((hasBounds.Bounds.Width + hasBounds.Bounds.Height) / 32);
                }
                Visibility = Visibility.Visible;
            });

            if (newTarget is IHasPosition hasPosition)
            {
                hasPosition.Position.Changed += Position_Changed;
                Position_Changed(hasPosition.Position);
            }
        }

        private void Position_Changed(PointF pos)
        {
            this.Do(() =>
            {
                Canvas.SetLeft(this, pos.X - Width / 2);
                Canvas.SetTop(this, pos.Y - Height / 2);
            });
        }

        public TargetControl()
        {
            InitializeComponent();
            Unloaded += (_, _) => Hero = null;
        }
    }
}
