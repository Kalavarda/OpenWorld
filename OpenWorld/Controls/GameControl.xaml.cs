﻿using System.Collections.Generic;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF.Controllers;
using OpenWorld.Controllers;
using OpenWorld.Models;

namespace OpenWorld.Controls
{
    public partial class GameControl
    {
        private Game _game;
        private HeroMoveController _heroMoveController;
        private PositionController _heroPositionController;
        private HeroToScreenCenterController _heroToScreenCenterController;

        private readonly IDictionary<IMapObject, PositionController> _positionControllers = new Dictionary<IMapObject, PositionController>();

        public Game Game
        {
            get => _game;
            set
            {
                if (_game == value)
                    return;

                if (_heroMoveController != null)
                    _heroMoveController.Dispose();
                if (_heroPositionController != null)
                    _heroPositionController.Dispose();
                if (_heroToScreenCenterController != null)
                    _heroToScreenCenterController.Dispose();

                if (_game != null)
                    _game.Map.LayerAdded -= Map_LayerAdded;

                _game = value;

                if (_game != null)
                {
                    _heroMoveController = new HeroMoveController(_bk, _game.Hero);
                    _heroPositionController = new PositionController(_heroControl, _game.Hero);
                    _heroControl.Hero = _game.Hero;

                    _heroToScreenCenterController = new HeroToScreenCenterController(_canvas, _game.Hero);

                    _game.Map.LayerAdded += Map_LayerAdded;
                    foreach (var mapLayer in _game.Map.Layers)
                        Map_LayerAdded(mapLayer);
                }
            }
        }

        private void Map_LayerAdded(MapLayer mapLayer)
        {
            if (mapLayer.IsHidden)
                return;

            mapLayer.ObjectAdded += MapLayer_ObjectAdded;
            foreach (var mapObject in mapLayer.Objects)
                MapLayer_ObjectAdded(mapObject);
        }

        private void MapLayer_ObjectAdded(IMapObject mapObject)
        {
            var control = App.ControlFactory.Create(mapObject);
            if (control == null)
                return;
            _mapCanvas.Children.Add(control);
            var positionController = new PositionController(control, mapObject);
            _positionControllers.Add(mapObject, positionController);
        }

        public GameControl()
        {
            InitializeComponent();

            _scaleTransform.ScaleX = _scaleTransform.ScaleY = Settings.Default.GameControlScale;

            Unloaded += GameControl_Unloaded;
        }

        private void GameControl_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Game = null;
        }
    }
}