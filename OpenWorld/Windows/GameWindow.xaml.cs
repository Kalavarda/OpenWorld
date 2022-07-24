using System.Windows;
using System.Windows.Controls;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF.Skills;
using OpenWorld.Controllers;
using OpenWorld.Controls;
using OpenWorld.Models;
using OpenWorld.Models.Hero;

namespace OpenWorld.Windows
{
    public partial class GameWindow
    {
        private readonly TargetSelectorController _targetSelectorController;
        private readonly SoundController _soundController;
        private readonly SkillController _skillController;
        private readonly HeroRespawnController _heroRespawnController;
        private readonly FightController _fightController;

        public Game Game { get; }

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(Game game): this()
        {
            Game = game;
            _gameControl.Game = game;

            var targetSelector = new TargetSelector(game.Hero, game.Map, 20);
            _targetSelectorController = new TargetSelectorController(this, game.Hero, targetSelector);

            _soundController = new SoundController(game.Map, game.Hero, App.SoundPlayer);
            _skillController = new SkillController(this, App.Processor, App.SkillBinds);
            _fightController = new FightController(game.Map, game.Hero);

            _heroRespawnController = new HeroRespawnController(game.Hero, game.Map);

            game.Hero.TargetChanged += Hero_TargetChanged;
            Hero_TargetChanged(null, game.Hero.Target);

            _heroBar.Hero = game.Hero;
            _heroBar.FightController = _fightController;
            _targetBar.FightController = _fightController;

            Unloaded += OnUnloaded;
        }

        private void Hero_TargetChanged(Unit oldTarget, Unit newTarget)
        {
            _targetBar.Visibility = newTarget != null ? Visibility.Visible : Visibility.Collapsed;
            _targetBar.Target = newTarget;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _targetSelectorController.Dispose();
            _soundController.Dispose();
            _skillController.Dispose();
            _heroRespawnController.Dispose();
            _fightController.Dispose();
            Game.Hero.TargetChanged -= Hero_TargetChanged;
        }

        public void ShowToolWindow(UserControl content, int width, int height, string title)
        {
            new Window
            {
                Content = content,
                Owner = this,
                ShowInTaskbar = false,
                Width = width,
                Height = height,
                WindowStyle = WindowStyle.ToolWindow,
                Title = title
            }.Show();
        }

        private void OnHeroDebugWindowClick(object sender, RoutedEventArgs e)
        {
            var control = new HeroDebugControl(Game.Hero);
            ShowToolWindow(control, 200, 100, nameof(Hero));
        }

        private void OnMobDebugWindowClick(object sender, RoutedEventArgs e)
        {
            var control = new MobDebugControl(Game);
            ShowToolWindow(control, 200, 200, nameof(Mob));
        }

        private void OnDebugWindowClick(object sender, RoutedEventArgs e)
        {
            new DebugWindow { Owner = this }.Show();
        }
    }
}
