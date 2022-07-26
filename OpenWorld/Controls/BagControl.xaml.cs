using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class BagControl
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

                _container.ItemContainer = _hero?.Bag;
            }
        }

        public BagControl()
        {
            InitializeComponent();
        }
    }
}
