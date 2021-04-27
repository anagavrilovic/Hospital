using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class StaticInventory : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string id;
        private static ObservableCollection<Inventory> allInventory;

        public static ObservableCollection<Inventory> Inventory
        {
            get;
            set;
        }

        InventoryStorage storage = new InventoryStorage();

        private string searchstr;

        private ICollectionView inventoryCollection;

        public ICollectionView InventoryCollection
        {
            get { return inventoryCollection; }
            set { inventoryCollection = value; }
        }


        public StaticInventory(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            this.id = id;

            InventoryStorage storage = new InventoryStorage();
            allInventory = storage.GetAll();
            // Inventory = storage.GetByRoomID(id);

            Inventory = new ObservableCollection<Inventory>();
            Inventory = storage.GetByRoomID(id);

            InventoryCollection = CollectionViewSource.GetDefaultView(Inventory);

            TransferInventoryStorage transferStorage = new TransferInventoryStorage();

            foreach(TransferInventory ti in transferStorage.GetAll())
            {
                if(ti.Date < DateTime.Now)
                {
                    ti.doTransfer();
                }
            }
        }

        private void searchInventory(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this.searchstr = textbox.Text;
                if (!string.IsNullOrEmpty(searchstr))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(dataGridInventory.ItemsSource);
                    view.Filter = new Predicate<object>(filter);
                    this.InventoryCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Inventory);
                    this.InventoryCollection.Refresh();
                }
            }
        }

        private void btnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            this.InventoryCollection.Refresh();
        }

      
        private bool filter(object item)
        {
            if (((Inventory)item).Name.Contains(searchstr) || ((Inventory)item).Id.Contains(searchstr) || ((Inventory)item).Price.ToString().Contains(searchstr) ||
                ((Inventory)item).RoomID.Contains(searchstr) || ((Inventory)item).Quantity.ToString().Contains(searchstr))
            {
                return true;
            }
            return false;
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

            if(selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete izabranu stavku",
                                                          "Brisanje stavke",
                                                           MessageBoxButton.YesNo,
                                                           MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (selectedItem != null)
                    {
                        Inventory.Remove(selectedItem);
                        storage.Delete(selectedItem.Id, selectedItem.RoomID);
                    }
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
                StringBuilder sb = new StringBuilder();
                sb.Append(selectedItem.Id);
                sb.Append("-");
                sb.Append(selectedItem.Name);
                transfer.nazivTxt.Text = sb.ToString();
                transfer.Show();
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
