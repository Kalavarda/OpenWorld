using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class HeroXpController: IDisposable
    {
        private const int BaseXP = 10;

        private readonly Hero _hero;
        private readonly ILevelMultiplier _levelMultiplier;

        public HeroXpController(Hero hero, ILevelMultiplier levelMultiplier)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _levelMultiplier = levelMultiplier ?? throw new ArgumentNullException(nameof(levelMultiplier));

            _hero.XP.Max = _levelMultiplier.GetValue(BaseXP, _hero.Level);
            _hero.XP.ValueChanged += XP_ValueChanged;
        }

        private void XP_ValueChanged(RangeF xp)
        {
            if (xp.IsMax)
            {
                _hero.Level++;
                xp.SetMin();
                _hero.XP.Max = _levelMultiplier.GetValue(BaseXP, _hero.Level);
            }
        }

        public void Dispose()
        {
            _hero.XP.ValueChanged -= XP_ValueChanged;
        }
    }
}
