using System.Windows;
using System.Windows.Media;
using Kalavarda.Primitives.Units.Buffs;
using Kalavarda.Primitives.Units.Fight;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.WPF;

namespace OpenWorld.Controls
{
    public partial class TargetBar
    {
        private ISelectable _target;
        private IFightController _fightController;

        public ISelectable Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;

                if (_target is IHasLevel hl)
                    hl.LevelChanged -= HasLevel_LevelChanged;

                _target = value;
                if (_target is ICreature creature)
                    _hpControl.Range = creature.HP;

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

                if (_target is IReadonlyHasBuffs hasBuffs)
                {
                    _buffsControl.HasBuffs = hasBuffs;
                    _buffsControl.Visibility = Visibility.Visible;
                }
                else
                    this.Do(() =>
                    {
                        _buffsControl.HasBuffs = null;
                        _buffsControl.Visibility = Visibility.Collapsed;
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
