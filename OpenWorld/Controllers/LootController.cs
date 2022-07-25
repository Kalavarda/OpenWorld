using System;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    public class LootController: IDisposable
    {
        private readonly Hero _hero;
        private readonly Map _map;

        private void Award(Mob mob)
        {
            _hero.XP.Value += 1;
        }

        public LootController(Hero hero, Map map)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _map = map ?? throw new ArgumentNullException(nameof(map));

            _map.LayerAdded += Map_LayerAdded;
            foreach (var mapLayer in _map.Layers)
                Map_LayerAdded(mapLayer);
        }

        private void Map_LayerAdded(MapLayer mapLayer)
        {
            mapLayer.ObjectAdded += MapLayer_ObjectAdded;
            foreach (var mapObject in mapLayer.Objects)
                MapLayer_ObjectAdded(mapObject);
        }

        private void MapLayer_ObjectAdded(IMapObject mapObject)
        {
            if (mapObject is Mob mob)
            {
                mob.Died += Mob_Died;
                mob.Disposing += Mob_Disposing;
            }
        }

        private void Mob_Disposing(Unit mob)
        {
            mob.Died -= Mob_Died;
            mob.Disposing -= Mob_Disposing;
        }

        private void Mob_Died(ICreature obj)
        {
            if (obj is Mob mob)
                Award(mob);
        }

        public void Dispose()
        {
            _map.LayerAdded -= Map_LayerAdded;
            foreach (var mapLayer in _map.Layers)
            {
                mapLayer.ObjectAdded -= MapLayer_ObjectAdded;
                foreach (var mapObject in mapLayer.Objects)
                    if (mapObject is Mob mob)
                    {
                        mob.Died -= Mob_Died;
                        mob.Disposing -= Mob_Disposing;
                    }
            }
        }
    }
}
