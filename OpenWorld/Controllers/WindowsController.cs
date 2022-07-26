using System;
using System.Windows;
using Kalavarda.Primitives.WPF.Binds;
using OpenWorld.Controls;
using OpenWorld.Models;
using OpenWorld.Windows;

namespace OpenWorld.Controllers
{
    internal class WindowsController: IDisposable
    {
        private readonly IKeyBindsController _keyBindsController;
        private readonly GameWindow _gameWindow;
        private readonly Game _game;
        private Window _bagWindow;

        public WindowsController(IKeyBindsController keyBindsController, GameWindow gameWindow, Game game)
        {
            _keyBindsController = keyBindsController ?? throw new ArgumentNullException(nameof(keyBindsController));
            _gameWindow = gameWindow ?? throw new ArgumentNullException(nameof(gameWindow));
            _game = game ?? throw new ArgumentNullException(nameof(game));

            _keyBindsController.BindActivated += KeyBindsController_BindActivated;
        }

        private void KeyBindsController_BindActivated(KeyBind bind)
        {
            switch (bind.Code)
            {
                case KeyBinds.Code_Bag:
                    if (_bagWindow == null)
                    {
                        var control = new BagControl { Hero = _game.Hero };
                        _bagWindow = _gameWindow.ShowToolWindow(control, 200, 300, "Сумка");
                        _gameWindow.Focus();
                    }
                    else
                    {
                        _bagWindow.Close();
                        _bagWindow = null;
                    }

                    break;
            }
        }

        public void Dispose()
        {
            _keyBindsController.BindActivated -= KeyBindsController_BindActivated;
        }
    }
}
