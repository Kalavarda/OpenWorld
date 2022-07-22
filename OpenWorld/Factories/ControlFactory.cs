using System;
using System.Windows.Controls;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using OpenWorld.Controls;

namespace OpenWorld.Factories
{
    internal class ControlFactory
    {
        public UserControl Create(IMapObject mapObject)
        {
            if (mapObject is Mob mob)
                return new MobControl { Mob = mob };

            if (mapObject is Spawn)
                return null;

            throw new NotImplementedException();
        }
    }
}
