using System.Threading;
using System.Windows;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.WPF.Skills;
using OpenWorld.Factories;

namespace OpenWorld
{
    public partial class App
    {
        private static readonly CancellationTokenSource _processorCancellationToken = new();

        internal static GameFactory GameFactory { get; }

        internal static ILevelMultiplier LevelMultiplier { get; } = new LevelMultiplier();

        internal static ControlFactory ControlFactory { get; } = new();

        internal static IProcessor Processor { get; } = new MultiProcessor(60, _processorCancellationToken.Token);

        internal static ISoundPlayer SoundPlayer { get; } = new SoundPlayer();

        internal static ISkillBinds SkillBinds { get; } = new HeroSkillBinds();

        static App()
        {
            GameFactory = new GameFactory(LevelMultiplier);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _processorCancellationToken.Cancel();

            base.OnExit(e);
        }
    }
}
