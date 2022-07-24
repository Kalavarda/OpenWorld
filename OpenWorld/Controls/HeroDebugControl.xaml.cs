using System;
using Kalavarda.Primitives.WPF;
using OpenWorld.Models;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class HeroDebugControl
    {
        private readonly Hero _hero;

        public HeroDebugControl()
        {
            InitializeComponent();
        }

        public HeroDebugControl(Hero hero): this()
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));

            _hero.Position.Changed += Position_Changed;
            Position_Changed(_hero.Position);
        }

        private void Position_Changed(Kalavarda.Primitives.Geometry.PointF pos)
        {
            this.Do(() =>
            {
                _tbPosition.Text = $"{MathF.Round(pos.X, 1)}; {MathF.Round(pos.Y, 1)}";
            });
        }
    }
}
