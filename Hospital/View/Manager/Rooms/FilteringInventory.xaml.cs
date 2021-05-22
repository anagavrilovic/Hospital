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
    public partial class FilteringInventory : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private InventoryStorage _staticInventoryStorage;
        private DynamicInventoryStorage _dynamicInventoryStorage;
        private RoomStorage _roomStorage;

        private int _minQuantityFilter;
        private int _maxQuantityFilter;


        public ObservableCollection<Room> Rooms { get; set; }
        private ObservableCollection<string> _staticInventoryItems;
        private ObservableCollection<string> _dynamicInventoryItems;

        public ObservableCollection<string> StaticInventoryItems
        {
            get => _staticInventoryItems;
            set
            {
                _staticInventoryItems = value;
                OnPropertyChanged("StaticInventoryItems");
            }
        }

        public ObservableCollection<string> DynamicInventoryItems
        {
            get => _dynamicInventoryItems;
            set
            {
                _dynamicInventoryItems = value;
                OnPropertyChanged("DynamicInventoryItems");
            }
        }

        public FilteringInventory()
        {
            InitializeComponent();
            this.DataContext = this;

            this._staticInventoryStorage = new InventoryStorage();
            this._dynamicInventoryStorage = new DynamicInventoryStorage();
            this._roomStorage = new RoomStorage();
            Rooms = _roomStorage.GetAll();
            
            InitializeStaticInventoryComboBox();
            InitializeDynamicInventoryComboBox();
        }

        private void InitializeStaticInventoryComboBox()
        {
            StaticInventoryItems = new ObservableCollection<string>();

            foreach (Inventory inventory in _staticInventoryStorage.GetAll())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(inventory.Id);
                sb.Append("-");
                sb.Append(inventory.Name);
                if(!StaticInventoryItems.Contains(sb.ToString()))
                  StaticInventoryItems.Add (sb.ToString());
            }
        }

        private void InitializeDynamicInventoryComboBox()
        {
            DynamicInventoryItems = new ObservableCollection<string>();

            foreach (DynamicInventory medicalSupply in _dynamicInventoryStorage.GetAll())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(medicalSupply.Id);
                sb.Append("-");
                sb.Append(medicalSupply.Name);
                if (!DynamicInventoryItems.Contains(sb.ToString()))
                    DynamicInventoryItems.Add(sb.ToString());
            }
        }

        private void IsFloorFilterSelected()
        {
            if (floorCB.SelectedItem == null)
                return;

            foreach(Room room in Rooms.ToList())
            {
                if (room.Floor != int.Parse(floorCB.Text))
                    Rooms.Remove(room);
            }
        }

        private void IsRoomTypeFilterSelected()
        {
            if (typeCB.SelectedItem == null)
                return;

            foreach(Room room in Rooms.ToList())
            {
               if(room.Type != (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text))
                    Rooms.Remove(room);
            }
        }

        private void IsStaticInventoryFilterSelected()
        {
            if (staticInventoryCB.SelectedItem == null)
                return;

            string selectedItemId = staticInventoryCB.Text.Split("-".ToCharArray())[0];

            foreach(Room room in Rooms.ToList())
            {
                bool condition = _staticInventoryStorage.GetByRoomID(room.Id).Contains(_staticInventoryStorage.GetOneByRoom(selectedItemId, room.Id));
                if (!condition)
                    Rooms.Remove(room);
            }
        }

        private void IsDynamicInventoryFilterSelected()
        {
            if (dynamicInventoryCB.SelectedItem == null)
                return;

            string selectedItemId = dynamicInventoryCB.Text.Split("-".ToCharArray())[0];

            foreach (Room room in Rooms.ToList())
            {
                bool condition = _dynamicInventoryStorage.GetByRoomID(room.Id).Contains(_dynamicInventoryStorage.GetOneByRoom(selectedItemId, room.Id));
                if (!condition)
                    Rooms.Remove(room);
            }
        }

        private void SetQuantityFilters()
        {
            _maxQuantityFilter = 0;

            if (quantityCB.Text.Contains(">"))
            {
                _minQuantityFilter = int.Parse(quantityCB.Text.Substring(1));
            }
            else
            {
                _minQuantityFilter = int.Parse(quantityCB.Text.Split("-".ToCharArray())[0]);
                _maxQuantityFilter = int.Parse(quantityCB.Text.Split("-".ToCharArray())[1]);
            }
        }

        private void FilterStaticInventory()
        {
            foreach (Room room in Rooms.ToList())
            {
                if (staticInventoryCB.SelectedItem != null)
                {
                    Inventory itemSelected = _staticInventoryStorage.GetOneByRoom(staticInventoryCB.Text.Split("-".ToCharArray())[0], room.Id);

                    bool condition;
                    if (_maxQuantityFilter == 0)
                        condition = itemSelected.Quantity > _minQuantityFilter;
                    condition = itemSelected.Quantity >= _minQuantityFilter && itemSelected.Quantity <= _maxQuantityFilter;

                    if (!condition)
                        Rooms.Remove(room);
                }
            }
        }

        private void FilterDynamicInventory()
        {
            foreach (Room room in Rooms.ToList())
            {
                if (dynamicInventoryCB.SelectedItem != null)
                {
                    DynamicInventory itemSelected = _dynamicInventoryStorage.GetOneByRoom(dynamicInventoryCB.Text.Split("-".ToCharArray())[0], room.Id);

                    bool condition;
                    if (_maxQuantityFilter == 0)
                        condition = itemSelected.Quantity > _minQuantityFilter;
                    else
                        condition = itemSelected.Quantity >= _minQuantityFilter && itemSelected.Quantity <= _maxQuantityFilter;

                    if (!condition)
                        Rooms.Remove(room);
                }
            }
        }

        private void IsQuantitiyFilterSelected()
        {
            if ((staticInventoryCB.SelectedItem == null && dynamicInventoryCB.SelectedItem == null) || quantityCB.SelectedItem == null)
                return;

            SetQuantityFilters();
            FilterStaticInventory();
            FilterDynamicInventory();
            
        }

        private void FinishFiltering(object sender, RoutedEventArgs e)
        {
            FilterRoomsDataGrid();
            NavigationService.Navigate(new RoomsWindow());

            RoomsWindow.Rooms = Rooms;
        }

        private void FilterRoomsDataGrid()
        {
            IsFloorFilterSelected();
            IsRoomTypeFilterSelected();
            IsStaticInventoryFilterSelected();
            IsDynamicInventoryFilterSelected();
            IsQuantitiyFilterSelected();
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

