﻿using System;
using Kalavarda.Primitives.WPF.Sound;
using OpenWorld.Models.Hero;
using OpenWorld.Models.Hero.Skills;
using OpenWorld.Models.Mobs.Spider;

namespace OpenWorld
{
    internal class SoundPlayer: SoundPlayerBase
    {
        protected override string GetFileName(string soundKey)
        {
            switch (soundKey)
            {
                case nameof(SpiderAttack):
                    return @"Mobs\Spider\Bite.mp3";

                case nameof(Hero) + nameof(SimpleStrike):
                    return @"Hero\SimpleStrike.mp3";

                default:
                    throw new NotImplementedException();
            }
        }

        public SoundPlayer() : base("Resources")
        {
        }
    }
}
