using System;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF;
using OpenWorld.Models;

namespace OpenWorld.Controls
{
    public partial class MobDebugControl
    {
        public MobDebugControl()
        {
            InitializeComponent();
        }

        public MobDebugControl(Game game): this()
        {
            game.Hero.TargetChanged += Hero_TargetChanged;
            Hero_TargetChanged(null, game.Hero.Target);
        }

        private void Hero_TargetChanged(Unit oldTarget, Unit newTarget)
        {
            if (oldTarget != null)
                oldTarget.Position.Changed -= Position_Changed;

            if (newTarget != null)
            {
                newTarget.Position.Changed += Position_Changed;
                Position_Changed(newTarget.Position);
            }
            else
                this.Do(() =>
                {
                    _tbPosition.Text = string.Empty;
                });
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
