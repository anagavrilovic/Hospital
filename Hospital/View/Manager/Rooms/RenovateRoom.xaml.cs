using Hospital.Model;
using Hospital.Services;
using Hospital.View.Manager.Rooms;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class RenovateRoom : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<string> WarehousesIDs { get; set; }
        public ObservableCollection<string> RoomIDs { get; set; }
        public ObservableCollection<string> RoomsToDestroy { get; set; }
        public ObservableCollection<string> RoomsToCreate { get; set; }

        private ObservableCollection<String> _roomsFromSameFloor;
        public ObservableCollection<String> RoomsFromSameFloor
        {
            get => _roomsFromSameFloor;
            set
            {
                _roomsFromSameFloor = value;
                OnPropertyChanged("RoomsFromSameFloor");
            }
        }

        private RoomService _roomService;
        private RoomRenovation _roomRenovation;
        public RoomRenovation RoomRenovation
        {
            get { return _roomRenovation; }
            set { _roomRenovation = value; OnPropertyChanged("RoomRenovation"); }
        }

        public RenovateRoom()
        {
            InitializeComponent();
            this.DataContext = this;
          
            RoomRenovation = new RoomRenovation();
            RoomsToDestroy = new ObservableCollection<string>();
            RoomsToCreate = new ObservableCollection<string>();
            this._roomService = new RoomService();

            FindRoomsFromSameFloor();
            InitializeComboBoxes();
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeRenovation();

            RoomRenovationService renovationService = new RoomRenovationService(RoomRenovation);
            renovationService.ProccessRenovationRequest();

            NavigationService.Navigate(new Renovations());
        }


        private void InitializeRenovation()
        {
            RoomRenovation.Room = _roomService.GetById(roomCB.Text);
            RoomRenovation.Room.SerializeInfo = false;
            RoomRenovation.WareHouse = _roomService.GetById(magacinCB.Text);
            if (RoomRenovation.WareHouse != null)
                RoomRenovation.WareHouse.SerializeInfo = false;
            SaveNewRooms();
        }

        private void SaveNewRooms()
        {
            foreach(Room r in RoomRenovation.RoomsCreatedDuringRenovation)
            {
                r.Floor = RoomRenovation.Room.Floor;
                r.SerializeInfo = true;
            }
        }

        private void BtnAddRoomForMerge(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = GetSelectedRoomForMerge();

            if (selectedRoom == null)
                return;

            selectedRoom.SerializeInfo = false;
            RoomRenovation.RoomsDestroyedDuringRenovation.Add(selectedRoom);
        }

        private void BtnDeleteRoomForMerge(object sender, RoutedEventArgs e)
        {
            Room selectedRoom = GetSelectedRoomForMerge();

            if (selectedRoom == null)
                return;

            RoomRenovation.RoomsDestroyedDuringRenovation.Remove(selectedRoom);
            RoomsToDestroy.Remove(allRoomsFromSameFloorList.SelectedItem.ToString());
            allRoomsFromSameFloorList.Items.Refresh();
        }

        private Room GetSelectedRoomForMerge()
        {
            if (allRoomsFromSameFloorList.SelectedItem == null)
                return null;

            String str = (String) allRoomsFromSameFloorList.SelectedItem;
            String[] splitedStr = str.Split('-');
            String roomID = splitedStr[0];
            RoomsToDestroy.Add(allRoomsFromSameFloorList.SelectedItem.ToString());

            return _roomService.GetById(roomID);
        }

        private void BtnRefrehRoomsForMerge(object sender, RoutedEventArgs e)
        {
            RoomsFromSameFloor = FindRoomsFromSameFloor();
            allRoomsFromSameFloorList.Items.Refresh();
        }

        private void SeparateRoomDuringRenovation(object sender, RoutedEventArgs e)
        {
            AddNewRoomDuringRenovation newRoom = new AddNewRoomDuringRenovation(RoomRenovation);
            NavigationService.Navigate(newRoom);
        }

        private ObservableCollection<String> FindRoomsFromSameFloor()
        {
            RoomsFromSameFloor = new ObservableCollection<String>();

            if (roomCB.SelectedItem != null)
            {
                Room RenovatingRoom = _roomService.GetById(roomCB.Text);
                foreach (Room r in _roomService.GetAll())
                    if (r.Floor == RenovatingRoom.Floor && !r.Id.Equals(RenovatingRoom.Id))
                        RoomsFromSameFloor.Add(r.Id + "-" + r.Name); 
            }
            else
            {
                foreach (Room r in _roomService.GetAll())
                    RoomsFromSameFloor.Add(r.Id + "-" + r.Name);
            }
            return RoomsFromSameFloor;
        }

        private void InitializeComboBoxes()
        {
            WarehousesIDs = new ObservableCollection<string>();
            RoomIDs = new ObservableCollection<string>();
            foreach (Room room in _roomService.GetAll())
            {
                RoomIDs.Add(room.Id);
                if (room.Type == RoomType.MAGACIN)
                    WarehousesIDs.Add(room.Id);
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }
    }
}
