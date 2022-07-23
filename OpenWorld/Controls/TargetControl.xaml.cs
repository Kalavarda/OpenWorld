using System.Windows;
using Kalavarda.Primitives.Units;

namespace OpenWorld.Controls
{
    public partial class TargetControl
    {
        private Unit _target;

        public Unit Target
        {
            get => _target;
            set
            {
                if (_target == value)
                    return;

                _target = value;
                _hpControl.Range = _target?.HP;
            }
        }

        public TargetControl()
        {
            InitializeComponent();
        }
    }
}
