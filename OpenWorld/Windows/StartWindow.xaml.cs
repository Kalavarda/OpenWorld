using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.EventAggregators;
using Kalavarda.Primitives.Utils;
using Kalavarda.Primitives.WPF.Controllers;
using OpenWorld.Controllers;
using OpenWorld.Processes;

namespace OpenWorld.Windows
{
    public partial class StartWindow
    {
        private CancellationTokenSource _cancellationTokenSource;
        private LootController _lootController;
        private HeroXpController _heroXpController;
        private MapEventAggregator _creatureEventAggregator;

        public StartWindow()
        {
            InitializeComponent();

            var assemblyName = typeof(StartWindow).Assembly.GetName();
            Title = assemblyName.Name + " " + assemblyName.Version;

            Loaded += StartWindow_Loaded;
        }

        private void StartWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Debugger.IsAttached)
                OnStartClick(null, EventArgs.Empty);
        }

        private void OnStartClick(object sender, EventArgs e)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var game = App.GameFactory.Create();
            App.Processor.Add(new HeroMoveProcess(game.Hero, _cancellationTokenSource.Token));
            App.Processor.Add(new MobsProcess(game.Hero, App.Processor, _cancellationTokenSource.Token));
            App.Processor.Add(new SpawnsProcess(game.Map, game.Map.Layers.First(), _cancellationTokenSource.Token));

            _creatureEventAggregator = new MapEventAggregator(game.Map);
            _lootController = new LootController(game.Hero, _creatureEventAggregator, App.LevelMultiplier, RandomImpl.Instance);
            _heroXpController = new HeroXpController(game.Hero, App.LevelMultiplier);

            var window = new GameWindow(game) { Owner = this };
            window.ControlBounds();
            window.Closing += (_, _) =>
            {
                _cancellationTokenSource.Cancel();
                _heroXpController.Dispose();
                _creatureEventAggregator.Dispose();
                _lootController.Dispose();
            };
            window.ShowDialog();
        }
    }
}
