using Hospital.Repositories;
using Hospital.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class EditMedicalSupply : Page, INotifyPropertyChanged
    {
        private DynamicInventory _dynamicInventoryItem;
        public DynamicInventory DynamicInventoryItem
        {
            get { return _dynamicInventoryItem; }
            set { _dynamicInventoryItem = value; OnPropertyChanged("DynamicInventoryItem"); }
        }

        public EditMedicalSupply(DynamicInventory item)
        {
            InitializeComponent();
            this.DataContext = this;
            DynamicInventoryItem = item;

            switch (DynamicInventoryItem.Units)
            {
                case UnitsType.kutije:  units.SelectedItem = kutije;  break;
                case UnitsType.trake:   units.SelectedItem = trake;   break;
                case UnitsType.flasice: units.SelectedItem = flasice; break;
            }
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            SaveEditedProperties();
            DynamicInventoryService inventoryService = new DynamicInventoryService(new DynamicInventoryFileRepository());
            inventoryService.EditItem(DynamicInventoryItem);

            NavigationService.Navigate(new DynamicInventoryView(DynamicInventoryItem.RoomID));
        }

        private void SaveEditedProperties()
        {
            idTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            nameTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            priceTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            quantityTxt.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            switch (units.SelectedIndex)
            {
                case 0: DynamicInventoryItem.Units = UnitsType.kutije; break;
                case 1: DynamicInventoryItem.Units = UnitsType.trake; break;
                case 2: DynamicInventoryItem.Units = UnitsType.flasice; break;
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(DynamicInventoryItem.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(DynamicInventoryItem.RoomID));
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
