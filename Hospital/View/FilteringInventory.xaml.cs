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
    /// Interaction logic for FilteringInventory.xaml
    /// </summary>
    public partial class FilteringInventory : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private InventoryStorage inventoryStorage;
        private MedicalSupplyStorage medicalSupplyStorage;

        private ObservableCollection<string> staticInventoryItems;
        private ObservableCollection<string> dynamicInventoryItems;

        public ObservableCollection<string> StaticInventoryItems
        {
            get
            {
                return staticInventoryItems;
            }

            set
            {
                staticInventoryItems = value;
                OnPropertyChanged("StaticInventoryItems");
            }
        }

        public ObservableCollection<string> DynamicInventoryItems
        {
            get
            {
                return dynamicInventoryItems;
            }

            set
            {
                dynamicInventoryItems = value;
                OnPropertyChanged("DynamicInventoryItems");
            }
        }

        public FilteringInventory()
        {
            InitializeComponent();
            this.DataContext = this;
            this.inventoryStorage = new InventoryStorage();
            this.medicalSupplyStorage = new MedicalSupplyStorage();
            StaticInventoryItems = new ObservableCollection<string>();
            DynamicInventoryItems = new ObservableCollection<string>();
            getAllStaticInventoryItems();
            getAllDynamicInventoryItems();
        }

        private void getAllStaticInventoryItems()
        {
            foreach(Inventory inventory in inventoryStorage.GetAll())
            {
                if(!StaticInventoryItems.Contains(inventory.Name))
                  StaticInventoryItems.Add(inventory.Name);
            }
        }

        private void getAllDynamicInventoryItems()
        {
            foreach(MedicalSupply medicalSupply in medicalSupplyStorage.GetAll())
            {
                if (!DynamicInventoryItems.Contains(medicalSupply.Name))
                    DynamicInventoryItems.Add(medicalSupply.Name);
            }
        }
        private void doFiltering(object sender, RoutedEventArgs e)
        {
            filterDataGrid();
            this.Close();
        }

        private void filterDataGrid()
        {

        }

        /*
        private bool IsComboBoxSelected()
        {

        }
        */

        private void cancelFiltering(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

