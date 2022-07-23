using System.Threading;
using System.Windows;
using Kalavarda.Primitives.Process;
using Kalavarda.Primitives.Sound;
using Kalavarda.Primitives.WPF.Sound;
using OpenWorld.Factories;

namespace OpenWorld
{
    public partial class App
    {
        private static readonly CancellationTokenSource _processorCancellationToken = new();

        internal static GameFactory GameFactory { get; } = new();

        internal static ControlFactory ControlFactory { get; } = new();

        internal static IProcessor Processor { get; } = new MultiProcessor(60, _processorCancellationToken.Token);

        internal static ISoundPlayer SoundPlayer { get; } = new SoundPlayer();

        protected override void OnExit(ExitEventArgs e)
        {
            _processorCancellationToken.Cancel();

            base.OnExit(e);
        }
    }
}
