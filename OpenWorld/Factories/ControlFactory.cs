using System;
using System.Windows.Controls;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF.Controls;
using Kalavarda.Primitives.WPF.Map;
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

            if (mapObject is MapTexture mapTexture)
                return new MapTextureControl { MapTexture = mapTexture };

            throw new NotImplementedException();
        }
    }
}
