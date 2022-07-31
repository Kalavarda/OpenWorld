using System.Windows;
using System.Windows.Controls;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.EventAggregators;
using Kalavarda.Primitives.Units.Fight;
using Kalavarda.Primitives.WPF;
using Kalavarda.Primitives.WPF.Binds;
using Kalavarda.Primitives.WPF.Controllers;
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
        private readonly MapEventAggregator _eventAggregator;
        private readonly KeyBindsController _keyBindsController;
        private readonly WindowsController _windowsController;
        private readonly UseItemController _useItemController;

        public Game Game { get; }

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(Game game): this()
        {
            Game = game;
            _gameControl.Game = game;

            _useItemController = new UseItemController(App.Processor, game.Hero);
            _eventAggregator = new MapEventAggregator(game.Map);
            _soundController = new SoundController(_eventAggregator, game.Hero, App.SoundPlayer);
            _keyBindsController = new KeyBindsController(this, App.KeyBinds);
            _skillController = new SkillController(_keyBindsController, App.Processor, App.SkillBinds, game.Hero);
            _fightController = new FightController(_eventAggregator, game.Hero);
            _windowsController = new WindowsController(_keyBindsController, this, game, _useItemController);

            _heroRespawnController = new HeroRespawnController(game.Hero, game.Map);

            game.Hero.TargetChanged += Hero_TargetChanged;
            Hero_TargetChanged(null, game.Hero.Target);

            _heroBar.Hero = game.Hero;
            _heroBar.FightController = _fightController;
            _targetBar.FightController = _fightController;

            var targetSelector = new TargetSelector(game.Hero, game.Map, Settings.Default.TargetMaxDistance, _fightController, _gameControl.MousePositionDetector);
            _targetSelectorController = new TargetSelectorController(game.Hero, targetSelector, _eventAggregator, _fightController, _keyBindsController);

            Unloaded += OnUnloaded;
        }

        private void Hero_TargetChanged(Unit oldTarget, Unit newTarget)
        {
            this.Do(() =>
            {
                _targetBar.Visibility = newTarget != null ? Visibility.Visible : Visibility.Collapsed;
                _targetBar.Target = newTarget;
            });
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            _targetSelectorController.Dispose();
            _soundController.Dispose();
            _skillController.Dispose();
            _heroRespawnController.Dispose();
            _fightController.Dispose();
            _eventAggregator.Dispose();
            _keyBindsController.Dispose();
            _windowsController.Dispose();
            _useItemController.Dispose();
            Game.Hero.TargetChanged -= Hero_TargetChanged;
        }

        public Window ShowToolWindow(UserControl content, int width, int height, string title)
        {
            var window = new Window
            {
                Content = content,
                Owner = this,
                ShowInTaskbar = false,
                Width = width,
                Height = height,
                WindowStyle = WindowStyle.ToolWindow,
                Title = title
            };
            window.ControlBounds(content.GetType().FullName);
            window.Show();
            return window;
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

        private void OnHelpClick(object sender, RoutedEventArgs e)
        {
            var window = new HelpWindow { Owner = this };
            window.ControlBounds();
            window.Show();
        }
    }
}
