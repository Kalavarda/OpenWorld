using System;
using System.Windows;
using Kalavarda.Primitives.Units.Interfaces;
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
        private readonly IUseItemController _useItemController;
        private Window _bagWindow;
        private Window _alchemyWindow;
        private Window _equipmentWindow;

        public WindowsController(IKeyBindsController keyBindsController, GameWindow gameWindow, Game game, IUseItemController useItemController)
        {
            _keyBindsController = keyBindsController ?? throw new ArgumentNullException(nameof(keyBindsController));
            _gameWindow = gameWindow ?? throw new ArgumentNullException(nameof(gameWindow));
            _game = game ?? throw new ArgumentNullException(nameof(game));
            _useItemController = useItemController ?? throw new ArgumentNullException(nameof(useItemController));

            _keyBindsController.BindActivated += KeyBindsController_BindActivated;
        }

        private void KeyBindsController_BindActivated(KeyBind bind)
        {
            switch (bind.Code)
            {
                case KeyBinds.Code_Bag:
                    if (_bagWindow == null)
                    {
                        var control = new BagControl(_game.Hero, _useItemController);
                        _bagWindow = _gameWindow.ShowToolWindow(control, 200, 300, "Сумка");
                        _gameWindow.Focus();
                    }
                    else
                    {
                        _bagWindow.Close();
                        _bagWindow = null;
                    }
                    break;

                case KeyBinds.Code_Alchemy:
                    if (_alchemyWindow == null)
                    {
                        var control = new AlchemyControl { Hero = _game.Hero };
                        _alchemyWindow = _gameWindow.ShowToolWindow(control, 200, 300, "Алхимия");
                        _gameWindow.Focus();
                    }
                    else
                    {
                        _alchemyWindow.Close();
                        _alchemyWindow = null;
                    }
                    break;

                case KeyBinds.Code_Equipment:
                    if (_equipmentWindow == null)
                    {
                        var control = new EquipmentControl(_game.Hero.Equipment);
                        _equipmentWindow = _gameWindow.ShowToolWindow(control, 200, 400, "Экипировкая");
                        _gameWindow.Focus();
                    }
                    else
                    {
                        _equipmentWindow.Close();
                        _equipmentWindow = null;
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
