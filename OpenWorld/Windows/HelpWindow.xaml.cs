using System;
using System.Linq;
using Kalavarda.Primitives.WPF.Binds;

namespace OpenWorld.Windows
{
    public partial class HelpWindow
    {
        public HelpWindow()
        {
            InitializeComponent();

            _tbKeyBindings.Text = string.Join(Environment.NewLine, App.KeyBinds.Binds.Select(kb => kb.Name + ":   " + ToString(kb)));
        }

        private static string ToString(KeyBind kb)
        {
            if (kb.Key != null)
                return kb.Key.Value.ToString();

            if (kb.MouseButton != null)
                return kb.MouseButton.Value.ToString().TrimStart('D');

            return "-";
        }
    }
}
