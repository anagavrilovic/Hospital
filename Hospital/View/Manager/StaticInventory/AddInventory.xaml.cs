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
        public  Inventory Inv { get; set;  }
        private InventoryStorage _inventoryStorage;

        public AddInventory(string id)
        {
            InitializeComponent();
            this.DataContext = this;

            Inv = new Inventory();
            Inv.RoomID = id;
            this._inventoryStorage = new InventoryStorage();
        }

        private void AcceptAddingButtonClick(object o, RoutedEventArgs e)
        {          
            bool isItemIDUnique = CheckUniquenessOfNewItemID();

            if(isItemIDUnique)
                _inventoryStorage.Save(Inv);
            else
                MessageBox.Show("Već postoji stavka sa unetom oznakom!");

            NavigationService.Navigate(new StaticInventoryView(Inv.RoomID));
        }

        private bool CheckUniquenessOfNewItemID()
        {
            foreach (Inventory inv in _inventoryStorage.GetByRoomID(Inv.RoomID))
            {
                if (inv.Id.Equals(Inv.Id))
                    return false;
            }
            return true;
        }

        private void CancelButtonClick(object o, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(Inv.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StaticInventoryView(Inv.RoomID));
        }
    }
}
