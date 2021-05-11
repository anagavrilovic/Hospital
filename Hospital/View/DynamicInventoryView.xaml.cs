﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class DynamicInventoryView : Page
    {
        private string _roomID;

        private static ObservableCollection<DynamicInventory> _supply;
        public static ObservableCollection<DynamicInventory> Supply 
        {
            get => _supply;
            set
            {
                _supply = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        private DynamicInventoryStorage _medicalSupplyStorage;

        private string _searchCriterion;
        public ICollectionView SupplyCollection { get; set; }

        public DynamicInventoryView(string id)
        {
            InitializeComponent();
            this.DataContext = this;
            this._roomID = id;

            _medicalSupplyStorage = new DynamicInventoryStorage();
            Supply = _medicalSupplyStorage.GetByRoomID(id);
            SupplyCollection = CollectionViewSource.GetDefaultView(Supply);
        }

        private void SearchMedicalSupply(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox != null)
            {
                this._searchCriterion = textbox.Text;
                if (!string.IsNullOrEmpty(_searchCriterion))
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(dataGridMedicalSupply.ItemsSource);
                    view.Filter = new Predicate<object>(Filter);
                    this.SupplyCollection.Refresh();
                }
                else
                {
                    ICollectionView view = CollectionViewSource.GetDefaultView(Supply);
                    this.SupplyCollection.Refresh();
                }
            }
        }

        private void ButtonSearchMouseDown(object sender, RoutedEventArgs e)
        {
            this.SupplyCollection.Refresh();
        }

        private bool Filter(object item)
        {
            if (((DynamicInventory)item).Name.Contains(_searchCriterion) || ((DynamicInventory)item).Id.Contains(_searchCriterion) || ((DynamicInventory)item).Price.ToString().Contains(_searchCriterion) ||
                ((DynamicInventory)item).RoomID.Contains(_searchCriterion) || ((DynamicInventory)item).Quantity.ToString().Contains(_searchCriterion))
            {
                return true;
            }
            return false;
        }

        private void AddItemButtonClick(object o, RoutedEventArgs e)
        {
            AddMedicalSupply addMS = new AddMedicalSupply(_roomID);
            NavigationService.Navigate(addMS);
        }

        private void EditItemButtonClick(object o, RoutedEventArgs e)
        {
            DynamicInventory selectedItem = (DynamicInventory)dataGridMedicalSupply.SelectedItem;
            if (selectedItem == null)
                return;

            EditMedicalSupply editMS= new EditMedicalSupply(selectedItem);
            NavigationService.Navigate(editMS);
        }

        private void DeleteItemButtonClick(object o, RoutedEventArgs e)
        {
            DynamicInventory selectedItem = (DynamicInventory) dataGridMedicalSupply.SelectedItem;
            if (selectedItem == null)
                return;

            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da izbrišete izabranu stavku",
                                         "Brisanje stavke", MessageBoxButton.YesNo, MessageBoxImage.Question);
           if (result == MessageBoxResult.Yes)
           {
              Supply.Remove(selectedItem);
              _medicalSupplyStorage.Delete(selectedItem.Id, selectedItem.RoomID);
           }
        }

        private void TransferItemButtonClick(object o, RoutedEventArgs e)
        {
            DynamicInventory selectedItem = (DynamicInventory)dataGridMedicalSupply.SelectedItem;
            if (selectedItem == null)
                return;

            TransferDynamicInventoryView transfer = new TransferDynamicInventoryView(selectedItem);
            transfer.nazivTxt.Text = selectedItem.Name;
            NavigationService.Navigate(transfer);
        }

        private void CreateReportButtonClick(object o, RoutedEventArgs e)
        {
            //TODO HCI 
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}


