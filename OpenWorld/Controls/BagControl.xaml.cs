using System;
using System.Windows.Controls;
using Kalavarda.Primitives.Skills;
using Kalavarda.Primitives.Units.Interfaces;
using Kalavarda.Primitives.Units.Items;
using OpenWorld.Models.Hero;

namespace OpenWorld.Controls
{
    public partial class BagControl
    {
        private readonly IUseItemController _useItemController;

        public BagControl()
        {
            InitializeComponent();

            _container.UseDefaultAction += UseDefaultAction;
            _container.ContextMenuOpening += OnContextMenuOpening;
        }

        public BagControl(Hero hero, IUseItemController useItemController): this()
        {
            _container.ItemContainer = hero.Bag;
            _useItemController = useItemController ?? throw new ArgumentNullException(nameof(useItemController));
        }

        private void UseDefaultAction(Item item)
        {
            if (item is IUsable usable)
                _useItemController.Use(usable);
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
