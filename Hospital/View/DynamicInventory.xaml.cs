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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DynamicInventory.xaml
    /// </summary>
    public partial class DynamicInventory : Page
    {
        private string id;

        public static ObservableCollection<MedicalSupply> Supply
        {
            get;
            set;
        }

        MedicalSupplyStorage storage = new MedicalSupplyStorage();

        private string searchstr;

        private ICollectionView supplyCollection;

        public ICollectionView SupplyCollection
        {
            get { return supplyCollection; }
            set { supplyCollection = value; }
        }


        public DynamicInventory(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            this.id = id;

            MedicalSupplyStorage storage = new MedicalSupplyStorage();
            Supply = new ObservableCollection<MedicalSupply>();
            Supply = storage.GetByRoomID(id);

            SupplyCollection = CollectionViewSource.GetDefaultView(Supply);
        }

        private void searchSupply(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this.searchstr = textbox.Text;
                if (!string.IsNullOrEmpty(searchstr))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(dataGridMedicalSupply.ItemsSource);
                    view.Filter = new Predicate<object>(filter);
                    this.SupplyCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Supply);
                    this.SupplyCollection.Refresh();
                }
            }
        }

        private void btnSearchMouseDown(object sender, RoutedEventArgs e)
        {
            this.SupplyCollection.Refresh();
        }


        private bool filter(object item)
        {
            if (((MedicalSupply)item).Name.Contains(searchstr) || ((MedicalSupply)item).Id.Contains(searchstr) || ((MedicalSupply)item).Price.ToString().Contains(searchstr) ||
                ((MedicalSupply)item).RoomID.Contains(searchstr) || ((MedicalSupply)item).Quantity.ToString().Contains(searchstr))
            {
                return true;
            }
            return false;
        }

        private void addItem(object o, RoutedEventArgs e)
        {
            AddMedicalSupply addMS = new AddMedicalSupply(id);
            NavigationService.Navigate(addMS);
        }

        private void editItem(object o, RoutedEventArgs e)
        {
            MedicalSupply selectedItem = (MedicalSupply)dataGridMedicalSupply.SelectedItem;

            if (selectedItem != null)
            {
                EditMedicalSupply editMS= new EditMedicalSupply(selectedItem);
                editMS.Owner = Application.Current.MainWindow;
                editMS.Show();
            }
        }

        private void deleteItem(object o, RoutedEventArgs e)
        {
            MedicalSupply selectedItem = (MedicalSupply) dataGridMedicalSupply.SelectedItem;
            if (selectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete izabranu stavku",
                                         "Brisanje stavke",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (selectedItem != null)
                    {
                        Supply.Remove(selectedItem);
                        storage.Delete(selectedItem.Id, selectedItem.RoomID);
                    }
                }
            }
        }

        private void transferItem(object o, RoutedEventArgs e)
        {
            MedicalSupply selectedItem = (MedicalSupply)dataGridMedicalSupply.SelectedItem;

            if (selectedItem != null)
            {
                PrebacivanjeOpreme transfer = new PrebacivanjeOpreme(selectedItem);
                transfer.Owner = Application.Current.MainWindow;
                transfer.nazivTxt.Text = selectedItem.Name;
                transfer.Show();
            }
        }

        private void createReport(object o, RoutedEventArgs e)
        {
            //TODO HCI 
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

    }

}



