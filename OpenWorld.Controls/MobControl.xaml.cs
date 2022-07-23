using System.Windows.Media;
using Kalavarda.Primitives.Units;
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
                    _mob.IsSelectedChanged -= Mob_IsSelectedChanged;

                _mob = value;

                if (_mob != null)
                {
                    Width = _mob.Bounds.Width;
                    Height = _mob.Bounds.Height;

                    _mob.IsSelectedChanged += Mob_IsSelectedChanged;
                    Mob_IsSelectedChanged(_mob);
                }
            }
        }

        private void Mob_IsSelectedChanged(Unit mob)
        {
            this.Do(() =>
            {
                if (_mob.IsSelected)
                    Background = Brushes.Maroon;
                else
                    Background = Brushes.Transparent;
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
