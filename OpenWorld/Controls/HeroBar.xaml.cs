using System.Windows.Media;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units.Fight;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.WPF;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class HeroBar
    {
        private Hero _hero;
        private IFightController _fightController;

        public Hero Hero
        {
            get => _hero;
            set
            {
                if (_hero == value)
                    return;

                if (_hero != null)
                    _hero.LevelChanged -= _hero_LevelChanged;

                _hero = value;
                _hpControl.Range = _hero?.HP;
                _xpControl.Range = _hero?.XP;

                if (_hero != null)
                {
                    _hero.LevelChanged += _hero_LevelChanged;
                    _hero_LevelChanged(_hero);
                }
            }
        }

        private void _hero_LevelChanged(IHasLevel hero)
        {
            this.Do(() =>
            {
                _tbLevel.Text = hero.Level.ToString();
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

        public HeroBar()
        {
            InitializeComponent();
            Unloaded += HeroBar_Unloaded;
        }

        private void HeroBar_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Hero = null;
            FightController = null;
        }
    }
}
