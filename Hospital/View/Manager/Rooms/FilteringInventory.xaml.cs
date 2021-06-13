using Hospital.Repositories;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class FilteringInventory : Page
    {
        public DTO.FilteringRoomsDTO FilteringRoomsDTO;

        public FilteringInventory()
        {
            InitializeComponent();
            this.DataContext = this;

            FilteringRoomsDTO = new DTO.FilteringRoomsDTO();
            
            InitializeStaticInventoryComboBox();
            InitializeDynamicInventoryComboBox();
        }

        private void InitializeStaticInventoryComboBox()
        {
            ObservableCollection<string>  StaticInventoryItems = new ObservableCollection<string>();

            Services.StaticInventoryService staticInventoryService = new Services.StaticInventoryService(new StaticInventoryFileRepository());
            foreach (Inventory inventory in staticInventoryService.GetAll())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(inventory.Id);
                sb.Append("-");
                sb.Append(inventory.Name);
                if(!StaticInventoryItems.Contains(sb.ToString()))
                  StaticInventoryItems.Add (sb.ToString());
            }
            staticInventoryCB.ItemsSource = StaticInventoryItems;
        }

        private void InitializeDynamicInventoryComboBox()
        {
            ObservableCollection<string>  DynamicInventoryItems = new ObservableCollection<string>();

            Services.DynamicInventoryService dynamicInventoryService = new Services.DynamicInventoryService(new DynamicInventoryFileRepository());
            foreach (DynamicInventory medicalSupply in dynamicInventoryService.GetAll())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(medicalSupply.Id);
                sb.Append("-");
                sb.Append(medicalSupply.Name);
                if (!DynamicInventoryItems.Contains(sb.ToString()))
                    DynamicInventoryItems.Add(sb.ToString());
            }
            dynamicInventoryCB.ItemsSource = DynamicInventoryItems;
        }

        private void GetFloorFilter()
        {
            if (floorCB.SelectedItem == null)
            {
                FilteringRoomsDTO.Floor = Services.RoomsFilteringService.NOT_SELECTED_FLOOR;
                return;
            }
            FilteringRoomsDTO.Floor = int.Parse(floorCB.Text);
        }

        private void GetRoomTypeFilter()
        {
            if (typeCB.SelectedItem == null)
            {
                FilteringRoomsDTO.Type = (RoomType)Services.RoomsFilteringService.NOT_SELECTED_ROOM_TYPE;
                return;
            }
            FilteringRoomsDTO.Type = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);          
        }

        private void GetStaticInventoryFilter()
        {
            if (staticInventoryCB.SelectedItem == null)
            {
                FilteringRoomsDTO.StaticInventoryId = Services.RoomsFilteringService.NOT_SELECTED_STATIC_INVENTORY;
                return;
            }
            FilteringRoomsDTO.StaticInventoryId = staticInventoryCB.Text.Split("-".ToCharArray())[0];
        }

        private void GetDynamicInventoryFilter()
        {
            if (dynamicInventoryCB.SelectedItem == null)
            {
                FilteringRoomsDTO.DynamicInventoryId = Services.RoomsFilteringService.NOT_SELECTED_DYNAMIC_INVENTORY;
                return;
            }
            FilteringRoomsDTO.DynamicInventoryId = dynamicInventoryCB.Text.Split("-".ToCharArray())[0];
        }

        private void SetQuantityFilters()
        {
            if (quantityCB.Text.Contains(">"))
            {
                FilteringRoomsDTO.MinQuantity = int.Parse(quantityCB.Text.Substring(1));
                FilteringRoomsDTO.MaxQuantity = Services.RoomsFilteringService.NOT_SELECTED_MAX_QUANTITY;
            }
            else
            {
                FilteringRoomsDTO.MinQuantity = int.Parse(quantityCB.Text.Split("-".ToCharArray())[0]);
                FilteringRoomsDTO.MaxQuantity = int.Parse(quantityCB.Text.Split("-".ToCharArray())[1]);
            }
        }

        private void GetQuantitiyFilter()
        {
            if ((staticInventoryCB.SelectedItem == null && dynamicInventoryCB.SelectedItem == null) || quantityCB.SelectedItem == null)
            {
                FilteringRoomsDTO.MinQuantity = Services.RoomsFilteringService.NOT_SELECTED_MIN_QUANTITY;
                FilteringRoomsDTO.MaxQuantity = Services.RoomsFilteringService.NOT_SELECTED_MAX_QUANTITY;
                return;
            }

            SetQuantityFilters();           
        }

        private void FinishFiltering(object sender, RoutedEventArgs e)
        {
            GetFilters();
            Services.RoomsFilteringService filteringService = new Services.RoomsFilteringService(FilteringRoomsDTO, new RoomFileRepository(), new StaticInventoryFileRepository(), new DynamicInventoryFileRepository());

            NavigationService.Navigate(new RoomsWindow());
            RoomsWindow.Rooms = new ObservableCollection<Room>(filteringService.FilterRooms());
        }

        private void GetFilters()
        {
            GetFloorFilter();
            GetRoomTypeFilter();
            GetStaticInventoryFilter();
            GetDynamicInventoryFilter();
            GetQuantitiyFilter();
        }

        private void CancelFiltering(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}

