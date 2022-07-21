using System;
using System.Windows.Controls;
using Kalavarda.Primitives.Abstract;
using OpenWorld.Controls;
using OpenWorld.Models;

namespace OpenWorld.Factories
{
    internal class ControlFactory
    {
        public UserControl Create(IMapObject mapObject)
        {
            if (mapObject is Mob mob)
                return new MobControl { Mob = mob };

            throw new NotImplementedException();
        }
    }
}
