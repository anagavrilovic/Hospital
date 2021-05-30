using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class AddInventory : Page
    {
        public  Inventory InventoryItem { get; set;  }

        public AddInventory(string id)
        {
            InitializeComponent();
            this.DataContext = this;

            InventoryItem = new Inventory();
            InventoryItem.RoomID = id;
        }

        private void AcceptAddingButtonClick(object o, RoutedEventArgs e)
        {
            StaticInventoryService inventoryService = new StaticInventoryService();
            if (!inventoryService.AddNewItem(InventoryItem))
                MessageBox.Show("Vec postoji stavka sa postojecom oznakom!");
         
            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }

        private void CancelButtonClick(object o, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }
    }
}
