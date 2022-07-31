using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class HeroControl
    {
        private Hero _hero;

        public Hero Hero
        {
            get => _hero;
            set
            {
                if (_hero == value)
                    return;

                _hero = value;

                if (_hero != null)
                {
                    Width = _hero.Bounds.Width;
                    Height = _hero.Bounds.Height;
                }
            }
        }

        public HeroControl()
        {
            InitializeComponent();
        }
    }
}
