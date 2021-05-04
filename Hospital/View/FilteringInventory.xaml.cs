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

        private InventoryStorage inventoryStorage;
        private MedicalSupplyStorage medicalSupplyStorage;
        private RoomStorage roomStorage;

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

        public ObservableCollection<Room> Rooms { get; set; }

        public FilteringInventory()
        {
            InitializeComponent();
            this.DataContext = this;
            this.inventoryStorage = new InventoryStorage();
            this.medicalSupplyStorage = new MedicalSupplyStorage();
            this.roomStorage = new RoomStorage();
            Rooms = roomStorage.GetAll();
            
            getAllStaticInventoryItemsForComboBox();
            getAllDynamicInventoryItemsForComboBox();
        }

        private void getAllStaticInventoryItemsForComboBox()
        {
            StaticInventoryItems = new ObservableCollection<string>();

            foreach (Inventory inventory in inventoryStorage.GetAll())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(inventory.Id);
                sb.Append("-");
                sb.Append(inventory.Name);
                if(!StaticInventoryItems.Contains(sb.ToString()))
                  StaticInventoryItems.Add (sb.ToString());
            }
        }

        private void getAllDynamicInventoryItemsForComboBox()
        {
            DynamicInventoryItems = new ObservableCollection<string>();

            foreach (MedicalSupply medicalSupply in medicalSupplyStorage.GetAll())
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(medicalSupply.Id);
                sb.Append("-");
                sb.Append(medicalSupply.Name);
                if (!DynamicInventoryItems.Contains(sb.ToString()))
                    DynamicInventoryItems.Add(sb.ToString());
            }
        }

        private void isFloorFilterSelected()
        {
            if (floorCB.SelectedItem == null)
                return;

            foreach(Room room in Rooms.ToList())
            {
                if (room.Floor != int.Parse(floorCB.Text))
                    Rooms.Remove(room);
            }
        }

        private void isRoomTypeSelected()
        {
            if (typeCB.SelectedItem == null)
                return;

            foreach(Room room in Rooms.ToList())
            {
               if(room.Type != (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text))
                {
                    Rooms.Remove(room);
                }
            }
        }

        private void isStaticInventoryItemSelected()
        {
            if (staticInventoryCB.SelectedItem == null)
                return;

            string selectedItemId = staticInventoryCB.Text.Split("-".ToCharArray())[0];

            foreach(Room room in Rooms.ToList())
            {
                bool condition = inventoryStorage.GetByRoomID(room.Id).Contains(inventoryStorage.GetOneByRoom(selectedItemId, room.Id));
                if (!condition)
                    Rooms.Remove(room);
            }
        }

        private void isDynamicInventoryItemSelected()
        {
            if (dynamicInventoryCB.SelectedItem == null)
                return;

            string selectedItemId = dynamicInventoryCB.Text.Split("-".ToCharArray())[0];

            foreach (Room room in Rooms.ToList())
            {
                bool condition = medicalSupplyStorage.GetByRoomID(room.Id).Contains(medicalSupplyStorage.GetOneByRoom(selectedItemId, room.Id));
                if (!condition)
                    Rooms.Remove(room);
            }
        }

        private void isQuantitiyFilterSelected()
        {
            if ((staticInventoryCB.SelectedItem == null && dynamicInventoryCB.SelectedItem == null) || quantityCB.SelectedItem == null)
                return;

            int minQuantityFilter;
            int maxQuantityFilter = 0;

            if(quantityCB.Text.Contains(">"))
            {
                minQuantityFilter = int.Parse(quantityCB.Text.Substring(1));
            }
            else
            {
                minQuantityFilter = int.Parse(quantityCB.Text.Split("-".ToCharArray())[0]);
                maxQuantityFilter = int.Parse(quantityCB.Text.Split("-".ToCharArray())[1]);
            }

            foreach(Room room in Rooms.ToList())
            {
                if (staticInventoryCB.SelectedItem != null)
                {
                    Inventory itemSelected = inventoryStorage.GetOneByRoom(staticInventoryCB.Text.Split("-".ToCharArray())[0], room.Id);
                    bool condition;
                    if (maxQuantityFilter==0)
                    {
                        condition = itemSelected.Quantity > minQuantityFilter;
                    }
                    else
                    {
                        condition = itemSelected.Quantity >= minQuantityFilter && itemSelected.Quantity <= maxQuantityFilter;
                    }

                    if (!condition)
                        Rooms.Remove(room);
                        
                }
                else if(dynamicInventoryCB.SelectedItem != null)
                {
                    MedicalSupply itemSelected = medicalSupplyStorage.GetOneByRoom(dynamicInventoryCB.Text.Split("-".ToCharArray())[0], room.Id);
                    bool condition;
                    if (maxQuantityFilter == 0)
                    {
                        condition = itemSelected.Quantity > minQuantityFilter;
                    }
                    else
                    {
                        condition = itemSelected.Quantity >= minQuantityFilter && itemSelected.Quantity <= maxQuantityFilter;
                    }

                    if (!condition)
                        Rooms.Remove(room);
                }
                   
            }
        }

        private void doFiltering(object sender, RoutedEventArgs e)
        {
            filterDataGrid();
            NavigationService.Navigate(new RoomsWindow());
            RoomsWindow.Rooms = Rooms;
        }

        private void filterDataGrid()
        {
            isFloorFilterSelected();
            isRoomTypeSelected();
            isStaticInventoryItemSelected();
            isDynamicInventoryItemSelected();
            isQuantitiyFilterSelected();
        }

        private void cancelFiltering(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomsWindow());
        }
    }
}

