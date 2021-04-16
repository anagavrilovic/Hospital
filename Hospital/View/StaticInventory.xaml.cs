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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for StaticInventory.xaml
    /// </summary>
    public partial class StaticInventory : Window
    {
        private string id;
        private static ObservableCollection<Inventory> allInventory;

        public static ObservableCollection<Inventory> Inventory
        {
            get;
            set;
        }

        InventoryStorage storage = new InventoryStorage();

        public StaticInventory(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            this.id = id;

            InventoryStorage storage = new InventoryStorage();
            allInventory = storage.GetAll();
            // Inventory = storage.GetByRoomID(id);

            //Console.WriteLine("SOBA  " + id);
            Inventory = new ObservableCollection<Inventory>();
            Inventory = storage.GetByRoomID(id);
            /*
            foreach (Inventory i in allInventory)
            {
                if (i.RoomID.Equals(id))
                {
                    Console.WriteLine("INV " + i.Id + " " + i.Name);
                    //  Console.WriteLine("++++");
                    Inventory.Add(i);
                }
            }*/
        }

        private void add(object o, RoutedEventArgs e)
        {
            AddInventory addInv = new AddInventory(id);
            addInv.Owner = Application.Current.MainWindow;
            addInv.Show();
        }

        private void edit(object o, RoutedEventArgs e)
        {
            Inventory selectedItem = (Inventory)dataGridInventory.SelectedItem;

            if (selectedItem != null)
            {
                EditInventory editInv = new EditInventory(selectedItem);
                editInv.Owner = Application.Current.MainWindow;
                editInv.Show();
            }
        }

        private void delete(object o, RoutedEventArgs e)
        {
            Inventory selectedItem = (Inventory)dataGridInventory.SelectedItem;

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete izabranu stavku",
                                                      "Brisanje stavke",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (selectedItem != null)
                {
                    Inventory.Remove(selectedItem);
                    storage.Delete(selectedItem.Id);
                }
            }
        }

        private void transfer(object o, RoutedEventArgs e)
        {
            Inventory selectedItem = (Inventory)dataGridInventory.SelectedItem;

            if(selectedItem != null)
            {
                PrebacivanjeInventara transfer = new PrebacivanjeInventara(selectedItem);
                transfer.Owner = Application.Current.MainWindow;
                transfer.nazivTxt.Text = selectedItem.Name;
                transfer.Show();
            }
        }
    }
}
