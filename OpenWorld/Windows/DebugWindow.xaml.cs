using System;
using System.Timers;
using Kalavarda.Primitives.Abstract;
using Kalavarda.Primitives.WPF;

namespace OpenWorld.Windows
{
    public partial class DebugWindow
    {
        private readonly IMousePositionDetector _mousePositionDetector;
        private readonly Timer _timer = new Timer(TimeSpan.FromSeconds(0.5).TotalMilliseconds);

        public DebugWindow()
        {
            InitializeComponent();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Do(() =>
            {
                var (x, y) = _mousePositionDetector.GetPosition();
                _tbMousePos.Text = MathF.Round(x, 1) + " " + MathF.Round(y, 1);
            });
        }

        private void DebugWindow_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
        }

        public DebugWindow(IMousePositionDetector mousePositionDetector): this()
        {
            _mousePositionDetector = mousePositionDetector ?? throw new ArgumentNullException(nameof(mousePositionDetector));
            Unloaded += DebugWindow_Unloaded;

            _timer.Elapsed += Timer_Elapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }
    }
}
