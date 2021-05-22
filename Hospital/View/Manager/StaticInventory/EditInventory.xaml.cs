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
        public Inventory InventoryItem {  get; set; }
        private InventoryStorage _inventoryStorage;

        public EditInventory(Inventory inventory)
        {
            InitializeComponent();
            this.DataContext = this;

            InventoryItem = inventory;
            this._inventoryStorage = new InventoryStorage();
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            SaveEditedProperties();
            _inventoryStorage.EditItem(InventoryItem);

            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }

        private void SaveEditedProperties()
        {
            idTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            nameTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            priceTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            quantityTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }
    }
}
