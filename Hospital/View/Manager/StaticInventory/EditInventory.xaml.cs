using Hospital.Repositories;
using Hospital.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class EditInventory : Page, INotifyPropertyChanged
    {
        private Inventory _inventoryItem;
        public Inventory InventoryItem
        {
            get { return _inventoryItem; }
            set { _inventoryItem = value; OnPropertyChanged("InventoryItem"); }
        }

        public EditInventory(Inventory inventory)
        {
            InitializeComponent();
            this.DataContext = this;

            InventoryItem = inventory;
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            SaveEditedProperties();
            StaticInventoryService inventoryService = new StaticInventoryService(new StaticInventoryFileRepository());
            inventoryService.EditItem(InventoryItem);

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
