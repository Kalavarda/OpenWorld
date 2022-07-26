using System.Windows.Controls;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class BagControl
    {
        private Hero _hero;

        public Hero Hero
        {
            get => _hero;
            set
            {
                if (_hero == value)
                    return;

                _hero = value;

                _container.ItemContainer = _hero?.Bag;
            }
        }

        public BagControl()
        {
            InitializeComponent();

            _container.UseDefaultAction += UseDefaultAction;
            _container.ContextMenuOpening += OnContextMenuOpening;
        }

        private void UseDefaultAction(Item item)
        {
            item.Equals(null);
        }

        private void OnContextMenuOpening(ContextMenu menu, Item item)
        {
            menu.Items.Clear();
            
            var miUse = new MenuItem { Header = "Использовать" };
            miUse.Click += MiUse_Click;
            menu.Items.Add(miUse);
        }

        private void MiUse_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UseDefaultAction(_container.SelectedItem);
        }
    }
}
