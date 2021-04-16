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
    /// <summary>
    /// Interaction logic for AddInventory.xaml
    /// </summary>
    public partial class AddInventory : Window
    {
        private ObservableCollection<Inventory> inventory = new ObservableCollection<Inventory>();
        private Inventory inv = new Inventory();

        public ObservableCollection<Inventory> Inventory
        {
            get { return inventory; }
            set { inventory = value; }
        }

        public Inventory Inv
        {
            get { return inv; }
            set { inv = value; }
        }
          

        public AddInventory(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            inv.RoomID = id;
        }

        private void accept(object o, RoutedEventArgs e)
        {
            inventory.Add(inv);
            InventoryStorage invStorage = new InventoryStorage();
            invStorage.Save(inv);
            StaticInventory.Inventory.Add(inv);

            this.Close();
        }

        private void cancel(object o, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
