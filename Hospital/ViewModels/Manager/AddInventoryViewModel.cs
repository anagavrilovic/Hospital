using Hospital.Commands.Manager;
using Hospital.Repositories;
using Hospital.Services;
using Hospital.View;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Manager
{
    public class AddInventoryViewModel : ViewModel
    {
        #region Properties

        private Inventory _inventoryItem;
        public Inventory InventoryItem
        {
            get { return _inventoryItem; }
            set { _inventoryItem = value; OnPropertyChanged("InventoryItem"); }
        }

        public NavigationService NavigationService { get; set; }

        #endregion

        #region Constructor
        public AddInventoryViewModel(NavigationService navigate, string id)
        {
            InventoryItem = new Inventory();
            InventoryItem.RoomID = id;
            AddInventoryCommand = new RelayCommand(Execute_AddInventoryCommand, CanExecuteCommands);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecuteCommands);
            BackCommand = new RelayCommand(Execute_BackCommand, CanExecuteCommands);
            NavigationService = navigate;
        }

        #endregion

        #region Commands

        public RelayCommand AddInventoryCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand BackCommand { get; set; }

        public void Execute_AddInventoryCommand(object obj)
        {
            StaticInventoryService inventoryService = new StaticInventoryService(new StaticInventoryFileRepository());
            inventoryService.AddNewItem(InventoryItem);

            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }

        public void Execute_BackCommand(object obj)
        {
            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }

        public void Execute_CancelCommand(object obj)
        {
            NavigationService.Navigate(new StaticInventoryView(InventoryItem.RoomID));
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        #endregion
    }
}
