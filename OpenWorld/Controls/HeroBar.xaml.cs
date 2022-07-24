using System.Windows.Media;
using Kalavarda.Primitives.WPF;
using OpenWorld.Controllers;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class HeroBar
    {
        private Hero _hero;
        private FightController _fightController;

        public Hero Hero
        {
            get => _hero;
            set
            {
                if (_hero == value)
                    return;

                _hero = value;
                _hpControl.Range = _hero?.HP;
            }
        }

        public FightController FightController
        {
            get => _fightController;
            set
            {
                if (_fightController == value)
                    return;

                _fightController = value;

                if (_fightController != null)
                {
                    _fightController.CurrentFightChanged += FightController_CurrentFightChanged;
                    FightController_CurrentFightChanged();
                }
            }
        }

        private void FightController_CurrentFightChanged()
        {
            this.Do(() =>
            {
                _border.BorderBrush = _fightController.CurrentFight != null
                    ? Brushes.Red
                    : Brushes.Transparent;
            });
        }

        public HeroBar()
        {
            InitializeComponent();
        }
    }
}
