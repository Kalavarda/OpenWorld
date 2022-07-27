using System;
using System.Windows;
using OpenWorld.Models.Hero;
using OpenWorld.Models.Items;

namespace OpenWorld.Controls
{
    public partial class AlchemyControl
    {
        public Hero Hero { get; set; }

        public AlchemyControl()
        {
            InitializeComponent();
        }

        private void OnBrewClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Hero.Bag.TryPull(ItemsRepository.SpiderLegs_Junk, 10, out var spiderLegs))
                {
                    var hpPotion = new HpPotion(ItemsRepository.HpPotion_Junk);
                    Hero.Bag.Add(hpPotion);
                }
                else
                    throw new Exception("Недостаточно ингредиентов");
            }
            catch (Exception exception)
            {
                App.ShowError(exception);
            }
        }
    }
}
