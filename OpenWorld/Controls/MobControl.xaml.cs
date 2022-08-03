using System.Windows.Media;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.WPF;

namespace OpenWorld.Controls
{
    public partial class MobControl
    {
        private Mob _mob;

        public Mob Mob
        {
            get => _mob;
            set
            {
                if (_mob == value)
                    return;

                if (_mob != null)
                    _mob.StateChanged -= Mob_StateChanged;

                _mob = value;

                if (_mob != null)
                {
                    Width = _mob.Bounds.Width;
                    Height = _mob.Bounds.Height;

                    _mob.StateChanged += Mob_StateChanged;
                    Mob_StateChanged(_mob, Mob.MobState.New, _mob.State);
                }
            }
        }

        private void Mob_StateChanged(Mob arg1, Mob.MobState arg2, Mob.MobState arg3)
        {
            this.Do(() =>
            {
                Opacity = _mob.IsDead ? 0.25 : 1;
            });
        }

        public MobControl()
        {
            InitializeComponent();

            Unloaded += MobControl_Unloaded;
        }

        private void MobControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Mob = null;
        }
    }
}
