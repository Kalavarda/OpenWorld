using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
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

        private void Hero_TargetChanged(ISelectable oldTarget, ISelectable newTarget)
        {
            if (oldTarget != null)
            {
                if (oldTarget is IHasPosition hasPosition)
                    hasPosition.Position.Changed -= Position_Changed;
                
                if (oldTarget is Mob mob)
                    mob.StateChanged -= Mob_StateChanged;
            }

            if (newTarget != null)
            {
                if (oldTarget is IHasPosition hasPosition)
                {
                    hasPosition.Position.Changed += Position_Changed;
                    Position_Changed(hasPosition.Position);
                }

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
