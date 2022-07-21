using System.Windows;
using System.Windows.Controls;
using OpenWorld.Controls;
using OpenWorld.Models;

namespace OpenWorld.Windows
{
    public partial class GameWindow
    {
        public Game Game { get; }

        public GameWindow()
        {
            InitializeComponent();
        }

        public GameWindow(Game game): this()
        {
            Game = game;
            _gameControl.Game = game;
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
    }
}
