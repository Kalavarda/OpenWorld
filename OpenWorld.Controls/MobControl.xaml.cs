using OpenWorld.Models;

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

                _mob = value;

                if (_mob != null)
                {
                    Width = _mob.Bounds.Width;
                    Height = _mob.Bounds.Height;
                }
            }
        }

        public MobControl()
        {
            InitializeComponent();
        }
    }
}
