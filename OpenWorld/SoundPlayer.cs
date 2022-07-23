using System;
using Kalavarda.Primitives.WPF.Sound;
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
                    return "Fireball_01.mp3";

                default:
                    throw new NotImplementedException();
            }
        }

        public SoundPlayer() : base("Resources")
        {
        }
    }
}
