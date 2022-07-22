using System.Windows;
using OpenWorld.Processes;

namespace OpenWorld.Windows
{
    public partial class StartWindow
    {
        public StartWindow()
        {
            InitializeComponent();

            var assemblyName = typeof(StartWindow).Assembly.GetName();
            Title = assemblyName.Name + " " + assemblyName.Version;
        }

        private void OnStartClick(object sender, RoutedEventArgs e)
        {
            var game = App.GameFactory.Create();
            App.Processor.Add(new HeroMoveProcess(game.Hero));
            App.Processor.Add(new MobsProcess(game.Hero, App.Processor));

            var window = new GameWindow(game) { Owner = this };
            window.ShowDialog();
        }
    }
}
