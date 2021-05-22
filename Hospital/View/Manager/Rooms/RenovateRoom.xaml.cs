using Hospital.Model;
using Hospital.View.Manager.Rooms;
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

        private ObservableCollection<string> _warehousesIDs;
        public ObservableCollection<string> WarehousesIDs
        {
            get => _warehousesIDs;
            set
            {
                _warehousesIDs = value;
                OnPropertyChanged("WarehousesIDs");
            }
        }
        private ObservableCollection<string> _roomIDs;
        public ObservableCollection<string> RoomIDs
        {
            get => _roomIDs;
            set
            {
                _roomIDs = value;
                OnPropertyChanged("RoomIDs");
            }
        }

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

        private RoomRenovationStorage _roomRenovationStorage;
        private AppointmentStorage _appointmentStorage;
        private RoomStorage _roomStorage;
        public RoomRenovation RoomRenovation { get; set; }

        public RenovateRoom()
        {
            InitializeComponent();
            this.DataContext = this;
          
            RoomRenovation = new RoomRenovation();
            this._roomStorage = new RoomStorage();
            this._appointmentStorage = new AppointmentStorage();
            this._roomRenovationStorage = new RoomRenovationStorage();

            FindRoomsFromSameFloor();
            InitializeComboBoxes();
        }

        private void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            InitializeRenovation();

            if (!IsRenovationAttributesValid())
                return;

            if (IsAnyAppointmentInRoomDuringRenovationPeriod())
                return;

            _roomRenovationStorage.Save(RoomRenovation);
            NavigationService.Navigate(new Renovations());
        }


        private void InitializeRenovation()
        {
            RoomRenovation.Room = _roomStorage.GetOne(roomCB.Text);
            RoomRenovation.Room.SerializeInfo = false;
            RoomRenovation.WareHouse = _roomStorage.GetOne(magacinCB.Text);
            RoomRenovation.WareHouse.SerializeInfo = false;
            SaveNewRooms();
        }

        private bool IsRenovationAttributesValid()
        {
            if (RoomRenovation.Room.Status == RoomStatus.RENOVIRA_SE)
            {
                MessageBox.Show("Izabrana sala se trenutno renovira!");
                return false;
            }

            if (RoomRenovation.StartDate >= RoomRenovation.EndDate)
            {
                MessageBox.Show("Pogrešan izbor datuma renoviranja!");
                return false;
            }

            return true;
        }

        private bool IsAnyAppointmentInRoomDuringRenovationPeriod()
        {
            foreach (Appointment appointment in _appointmentStorage.GetAll())
            {
                if (appointment.DateTime > RoomRenovation.StartDate && appointment.DateTime < RoomRenovation.EndDate + new TimeSpan(23, 59, 59) && appointment.Room.Id.Equals(RoomRenovation.Room.Id))
                {
                    MessageBox.Show("U izabranom periodu postoje zakazani termini pa nije moguće zakazati renoviranje!");
                    return true;
                }
            }
            return false;
        }

        private void SaveNewRooms()
        {
            foreach(Room r in RoomRenovation.RoomsCreatedDuringRenovation)
            {
                r.Floor = RoomRenovation.Room.Floor;
                r.SerializeInfo = true;
            }
        }

        private void EditRoomsOrganization(object sender, RoutedEventArgs e)
        {
            if (roomCB.SelectedItem == null)
                return;

            View.Manager.Rooms.ChangingRoomsPlan plan = new View.Manager.Rooms.ChangingRoomsPlan(_roomStorage.GetOne(roomCB.Text));
            NavigationService.Navigate(plan);
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
            allRoomsFromSameFloorList.Items.Refresh();
        }

        private Room GetSelectedRoomForMerge()
        {
            if (allRoomsFromSameFloorList.SelectedItem == null)
                return null;

            String str = (String) allRoomsFromSameFloorList.SelectedItem;
            String[] splitedStr = str.Split('-');
            String roomID = splitedStr[0];

            return _roomStorage.GetOne(roomID);
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
                Room RenovatingRoom = _roomStorage.GetOne(roomCB.Text);
                foreach (Room r in _roomStorage.GetAll())
                    if (r.Floor == RenovatingRoom.Floor && !r.Id.Equals(RenovatingRoom.Id))
                        RoomsFromSameFloor.Add(r.Id + "-" + r.Name); 
            }
            else
            {
                foreach (Room r in _roomStorage.GetAll())
                    RoomsFromSameFloor.Add(r.Id + "-" + r.Name);
            }
            return RoomsFromSameFloor;
        }

        private void InitializeComboBoxes()
        {
            WarehousesIDs = new ObservableCollection<string>();
            RoomIDs = new ObservableCollection<string>();
            foreach (Room room in _roomStorage.GetAll())
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
