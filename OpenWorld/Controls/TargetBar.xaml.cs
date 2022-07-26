using System.Windows;
using System.Windows.Media;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Fight;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.WPF;

namespace OpenWorld.Controls
{
    public partial class TargetBar
    {
        private Unit _target;
        private IFightController _fightController;

        public Unit Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;

                if (_target is IHasLevel hl)
                    hl.LevelChanged -= HasLevel_LevelChanged;

                _target = value;
                _hpControl.Range = _target?.HP;

                if (_target is IHasLevel hasLevel)
                {
                    hasLevel.LevelChanged += HasLevel_LevelChanged;
                    HasLevel_LevelChanged(hasLevel);
                }
                else
                    this.Do(() =>
                    {
                        _tbLevel.Visibility = Visibility.Collapsed;
                    });
            }
        }

        private void HasLevel_LevelChanged(IHasLevel target)
        {
            this.Do(() =>
            {
                _tbLevel.Visibility = Visibility.Visible;
                _tbLevel.Text = target.Level.ToString();
            });
        }

        public IFightController FightController
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
