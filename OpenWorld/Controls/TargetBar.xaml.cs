using System.Windows;
using System.Windows.Media;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF;
using OpenWorld.Controllers;

namespace OpenWorld.Controls
{
    public partial class TargetBar
    {
        private Unit _target;
        private FightController _fightController;

        public Unit Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;

                _target = value;
                _hpControl.Range = _target?.HP;
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

        public TargetBar()
        {
            InitializeComponent();
        }
    }
}
