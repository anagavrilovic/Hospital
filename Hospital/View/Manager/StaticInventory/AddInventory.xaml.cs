using Hospital.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class AddInventory : Page, INotifyPropertyChanged
    {
        private Inventory _inventoryItem;
        public Inventory InventoryItem
        {
            get { return _inventoryItem; }
            set { _inventoryItem = value; OnPropertyChanged("InventoryItem"); }
        }

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
