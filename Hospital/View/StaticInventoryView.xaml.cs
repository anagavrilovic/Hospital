﻿using Hospital.Model;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    public partial class StaticInventoryView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _roomID;

        private static ObservableCollection<Inventory> _inventory;
        public static ObservableCollection<Inventory> Inventory 
        {
            get => _inventory; 
            set
            {
                _inventory = value;
                NotifyStaticPropertyChanged();
            }
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged([CallerMemberName] string name = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(name));
        }

        private InventoryStorage _inventoryStorage;
        private string _searchCriterion;
        public ICollectionView InventoryCollection { get; set; }

        public StaticInventoryView (string id)
        {
            InitializeComponent();
            this.DataContext = this;
            this._roomID = id;

            _inventoryStorage = new InventoryStorage();
            Inventory = _inventoryStorage.GetByRoomID(id);
            InventoryCollection = CollectionViewSource.GetDefaultView(Inventory);

            TransferInventoryStorage transferStorage = new TransferInventoryStorage();
            foreach(TransferInventory ti in transferStorage.GetAll())
            {
                if(ti.TransferDate < DateTime.Now)
                {
                    // ti.StartTransfer();
                    ti.WaitUntilTransferDate();
                }
            }
        }

        private void SearchInventory(object sender, TextChangedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox == null)
                return;

            this._searchCriterion = textbox.Text;
            if (!string.IsNullOrEmpty(_searchCriterion))
            {
                ICollectionView view = CollectionViewSource.GetDefaultView(dataGridInventory.ItemsSource);
                view.Filter = new Predicate<object>(Filter);
                InventoryCollection.Refresh();
            }
            else
            {
                 ICollectionView view = CollectionViewSource.GetDefaultView(Inventory);
                 InventoryCollection.Refresh();
            }
        }

        private void ButtonSearchMouseDown(object sender, RoutedEventArgs e)
        {
            this.InventoryCollection.Refresh();
        }

      
        private bool Filter(object item)
        {
            if (((Inventory)item).Name.Contains(_searchCriterion) || ((Inventory)item).Id.Contains(_searchCriterion) || ((Inventory)item).Price.ToString().Contains(_searchCriterion) ||
                ((Inventory)item).RoomID.Contains(_searchCriterion) || ((Inventory)item).Quantity.ToString().Contains(_searchCriterion))
            {
                return true;
            }
            return false;
        }

        private void AddButtonClick(object o, RoutedEventArgs e)
        {
            AddInventory addInv = new AddInventory(_roomID);
            NavigationService.Navigate(addInv);
        }

        private void EditButtonClick(object o, RoutedEventArgs e)
        {
            Inventory selectedItem = (Inventory)dataGridInventory.SelectedItem;
            if (selectedItem == null)
                return;
            
            EditInventory editInv = new EditInventory(selectedItem);
            NavigationService.Navigate(editInv);
        }

        private void DeleteButtonClick(object o, RoutedEventArgs e)
        {
            Inventory selectedItem = (Inventory)dataGridInventory.SelectedItem;
            if (selectedItem == null)
                return;

            NavigationService.Navigate(new DeleteInventory(selectedItem));
        }

        private void TransferButtonClick(object o, RoutedEventArgs e)
        {
            Inventory selectedItem = (Inventory)dataGridInventory.SelectedItem;
            if (selectedItem == null)
                return;

            TransferStaticInventory transfer = new TransferStaticInventory(selectedItem);
            StringBuilder sb = new StringBuilder();
            sb.Append(selectedItem.Id);
            sb.Append("-");
            sb.Append(selectedItem.Name);
            transfer.nazivTxt.Text = sb.ToString();
            NavigationService.Navigate(transfer);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

    }
}
