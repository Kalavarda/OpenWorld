﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using Kalavarda.Primitives.Units;
using OpenWorld.Processes;

namespace OpenWorld.Windows
{
    public partial class StartWindow
    {
        private CancellationTokenSource _cancellationTokenSource;

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

            ((HeroSkillBinds)App.SkillBinds).Hero = game.Hero; // TODO

            var window = new GameWindow(game) { Owner = this };
            window.Closing += (_, _) => _cancellationTokenSource.Cancel();
            window.ShowDialog();
        }
    }
}
