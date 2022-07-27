using System;
using System.Threading;
using System.Windows;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.Units;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
using Kalavarda.Primitives.WPF.Binds;
using OpenWorld.Factories;

namespace OpenWorld
{
    public partial class App
    {
        private static readonly CancellationTokenSource _processorCancellationToken = new();

        internal static GameFactory GameFactory { get; }

        internal static ILevelMultiplier LevelMultiplier { get; } = new LevelMultiplier(MathF.Sqrt(MathF.Sqrt(2)));

        internal static ControlFactory ControlFactory { get; } = new();

        internal static IProcessor Processor { get; } = new MultiProcessor(60, _processorCancellationToken.Token);

        internal static ISoundPlayer SoundPlayer { get; } = new SoundPlayer();

        internal static IKeyBinds KeyBinds { get; } = new KeyBinds();

        internal static ISkillBinds SkillBinds { get; } = new SkillBinds();

        internal static IItemsRepository ItemsRepository { get; } = new ItemsRepository();

        static App()
        {
            GameFactory = new GameFactory(LevelMultiplier);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _processorCancellationToken.Cancel();

            base.OnExit(e);
        }

        public static void ShowError(Exception exception)
        {
            MessageBox.Show(exception.GetBaseException().Message);
        }
    }
}
