using System;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Models;
using OpenWorld.Processes;

namespace OpenWorld.Items
{
    public class HpPotion: Item, IUsable
    {
        private static readonly TimeSpan Period = TimeSpan.FromSeconds(1);

        public HpPotion(ItemType type) : base(type)
        {
        }

        public IProcess Use(ISkilled actor)
        {
            if (actor is Unit unit)
                return new ChangeHpProcess(unit, unit, GetHpDelta(), Period, GetTimes(), this);
            else
                throw new NotSupportedException();
        }

        private ushort GetTimes()
        {
            switch (Type.Quality)
            {
                case ItemQuality.Junk:
                    return 10;
                case ItemQuality.Ordinary:
                    return 20;
                case ItemQuality.Good:
                    return 40;
                case ItemQuality.Rare:
                    return 80;
                case ItemQuality.Epic:
                    return 160;
                case ItemQuality.Legendary:
                    return 320;
                default:
                    throw new NotImplementedException();
            }
        }

        private float GetHpDelta()
        {
            switch (Type.Quality)
            {
                case ItemQuality.Junk:
                    return 1f;
                case ItemQuality.Ordinary:
                    return 2f;
                case ItemQuality.Good:
                    return 4f;
                case ItemQuality.Rare:
                    return 8f;
                case ItemQuality.Epic:
                    return 16f;
                case ItemQuality.Legendary:
                    return 32f;
                default:
                    throw new NotImplementedException();
            }
        }

        public override Item Clone()
        {
            return new HpPotion(Type)
            {
                Count = Count
            };
        }
    }
}
