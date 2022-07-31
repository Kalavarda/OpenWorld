using Kalavarda.Primitives.Units.Items;

namespace OpenWorld.Controls
{
    public partial class EquipmentItemControl
    {
        private IEquipmentItem _equipmentItem;

        public IEquipmentItem EquipmentItem
        {
            get => _equipmentItem;
            set
            {
                if (_equipmentItem == value)
                    return;

                _equipmentItem = value;
                _itemControl.DataContext = _equipmentItem;
            }
        }

        public EquipmentItemControl()
        {
            InitializeComponent();
        }
    }
}
