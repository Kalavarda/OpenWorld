using System;
using Kalavarda.Primitives;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Units.Interfaces;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controllers
{
    internal class HeroXpController: IDisposable
    {
        private readonly int _baseXp = Settings.Default.HeroXpBase;

        private readonly Hero _hero;
        private readonly ILevelMultiplier _levelMultiplier;

        public HeroXpController(Hero hero, ILevelMultiplier levelMultiplier)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _levelMultiplier = levelMultiplier ?? throw new ArgumentNullException(nameof(levelMultiplier));

            _hero.XP.Max = _levelMultiplier.GetValue(_baseXp, _hero.Level);
            _hero.XP.ValueChanged += XP_ValueChanged;
        }

        private void XP_ValueChanged(RangeF xp)
        {
            if (xp.IsMax)
            {
                _hero.Level++;
                xp.SetMin();
                _hero.XP.Max = _levelMultiplier.GetValue(_baseXp, _hero.Level);
            }
        }

        public void Dispose()
        {
            _hero.XP.ValueChanged -= XP_ValueChanged;
        }
    }
}
