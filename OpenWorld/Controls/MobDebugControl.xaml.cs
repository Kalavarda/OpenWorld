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

            if (game.Hero.Target is Mob mob)
                mob.StateChanged += Mob_StateChanged;
        }

        private void Mob_StateChanged(Mob mob, Mob.MobState oldState, Mob.MobState newState)
        {
            this.Do(() =>
            {
                _tbState.Text = newState.ToString();
            });
        }

        private void Hero_TargetChanged(Unit oldTarget, Unit newTarget)
        {
            if (oldTarget != null)
            {
                oldTarget.Position.Changed -= Position_Changed;
                
                if (oldTarget is Mob mob)
                    mob.StateChanged -= Mob_StateChanged;
            }

            if (newTarget != null)
            {
                newTarget.Position.Changed += Position_Changed;
                Position_Changed(newTarget.Position);

                if (newTarget is Mob mob)
                {
                    mob.StateChanged += Mob_StateChanged;
                    Mob_StateChanged(mob, Mob.MobState.New, mob.State);
                }
            }
            else
                this.Do(() =>
                {
                    _tbPosition.Text = string.Empty;
                    _tbState.Text = string.Empty;
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
