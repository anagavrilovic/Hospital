﻿using Hospital.Services;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class AddMedicalSupply : Page
    {
        public DynamicInventory DynamicInventoryItem  { get; set; }

        public AddMedicalSupply(string id)
        {
            InitializeComponent();
            this.DataContext = this;
      
            DynamicInventoryItem = new DynamicInventory();
            DynamicInventoryItem.RoomID = id;
        }

        private void AcceptAddingButtonClick(object o, RoutedEventArgs e)
        {
            switch (Units.SelectedIndex)
            {
                case 0: DynamicInventoryItem.Units = UnitsType.kutije;   break;
                case 1: DynamicInventoryItem.Units = UnitsType.trake;    break;
                case 2: DynamicInventoryItem.Units = UnitsType.flasice;  break;
            }

            DynamicInventoryService inventoryService = new DynamicInventoryService();
            inventoryService.AddNewItem(DynamicInventoryItem);
 
            NavigationService.Navigate(new DynamicInventoryView(DynamicInventoryItem.RoomID));
        }

        private void CancelButtonClick(object o, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(DynamicInventoryItem.RoomID));
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DynamicInventoryView(DynamicInventoryItem.RoomID));
        }
    }
}
