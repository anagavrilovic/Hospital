using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    public partial class EditInventory : Page
    {
        public Inventory Inv {  get; set; }

        public EditInventory(Inventory inventory)
        {
            InitializeComponent();
            this.DataContext = this;
            Inv = inventory;
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            oznakaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            nazivTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            cenaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            kolicinaTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            InventoryStorage inventoryStorage = new InventoryStorage();
            inventoryStorage.doSerialization();

            NavigationService.Navigate(new StaticInventory(Inv.RoomID));
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventory(Inv.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventory(Inv.RoomID));
        }
    }
}
