using Hospital.Services;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class DeleteInventory : Page
    {
        public Inventory InventoryForDeleting;

        public DeleteInventory(Inventory inventory)
        {
            InitializeComponent();
            InventoryForDeleting = inventory;
        }

        private void AcceptDeleting(object sender, RoutedEventArgs e)
        {
            StaticInventoryService inventoryService = new StaticInventoryService();
            inventoryService.DeleteItem(InventoryForDeleting);

            NavigationService.Navigate(new StaticInventoryView(InventoryForDeleting.RoomID));
        }

        private void CancelDeleting(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(InventoryForDeleting.RoomID));
        }
    }
}
