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
    /// <summary>
    /// Interaction logic for RenovateRoom.xaml
    /// </summary>
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

        private ObservableCollection<string> warehousesIDs;
        public ObservableCollection<string> WarehousesIDs
        {
            get
            {
                return warehousesIDs;
            }

            set
            {
                warehousesIDs = value;
                OnPropertyChanged("WarehousesIDs");
            }
        }
        private ObservableCollection<string> roomIDs;
        public ObservableCollection<string> RoomIDs
        {
            get
            {
                return roomIDs;
            }

            set
            {
                roomIDs = value;
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

        //private Room room;
        private RoomRenovation roomRenovation;
        private RoomStorage roomStorage;

        public RoomRenovation RoomRenovation
        {
            get { return roomRenovation;  }
            set { roomRenovation = value; }
        }


        public RenovateRoom()
        {
            InitializeComponent();
            this.DataContext = this;
            // this.room = room;
            this.roomRenovation = new RoomRenovation();
            this.roomStorage = new RoomStorage();

            FindRoomsFromSameFloor();
            WarehousesIDs = new ObservableCollection<string>();
            RoomIDs = new ObservableCollection<string>();
            foreach (Room room in roomStorage.GetAll())
            {
                RoomIDs.Add(room.Id);
                if (room.Type == RoomType.MAGACIN)
                    WarehousesIDs.Add(room.Id);
            }
        }

        private void accept(object sender, RoutedEventArgs e)
        { 
            roomRenovation.Room = roomStorage.GetOne(roomCB.Text);
            roomRenovation.Room.SerializeInfo = false;
            roomRenovation.WareHouse = roomStorage.GetOne(magacinCB.Text);
            roomRenovation.WareHouse.SerializeInfo = false;

            if (roomRenovation.Room.Status == RoomStatus.RENOVIRA_SE)
            {
                MessageBox.Show("Izabrana sala se trenutno renovira!");
            }

            if (roomRenovation.StartDate >= roomRenovation.EndDate)
            {
                MessageBox.Show("Pogrešan izbor datuma renoviranja!");
                return;
            }

            SaveNewRoomsInfo();

            AppointmentStorage appointmentStorage = new AppointmentStorage();

            bool isAnyAppointmentInRenovationPeriod = false;

            foreach(Appointment appointment in appointmentStorage.GetAll())
            {
                if(appointment.DateTime > roomRenovation.StartDate && appointment.DateTime < roomRenovation.EndDate + new TimeSpan(23,59,59) && appointment.Room.Id.Equals(roomRenovation.Room.Id))
                {
                    isAnyAppointmentInRenovationPeriod = true;
                    break;
                }
            }

            if(!isAnyAppointmentInRenovationPeriod)
            {
                RoomRenovationStorage storage = new RoomRenovationStorage();
                storage.Save(roomRenovation);

                NavigationService.Navigate(new Renovations());
            }
            else 
            {
                MessageBox.Show("U izabranom periodu postoje zakazani termini pa nije moguće zakazati renoviranje!");
                return;
            }
        }

        private void SaveNewRoomsInfo()
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

            View.Manager.Rooms.ChangingRoomsPlan plan = new View.Manager.Rooms.ChangingRoomsPlan(roomStorage.GetOne(roomCB.Text));
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

            selectedRoom.SerializeInfo = false;
            RoomRenovation.RoomsDestroyedDuringRenovation.Remove(selectedRoom);
        }

        private Room GetSelectedRoomForMerge()
        {
            if (allRoomsFromSameFloorList.SelectedItem == null)
                return null;

            String str = (String) allRoomsFromSameFloorList.SelectedItem;
            String[] splitedStr = str.Split('-');
            String roomID = splitedStr[0];

            return roomStorage.GetOne(roomID);
        }

        private void BtnRefrehRoomsForMerge(object sender, RoutedEventArgs e)
        {
            RoomsFromSameFloor = FindRoomsFromSameFloor();
            allRoomsFromSameFloorList.Items.Refresh();
        }

        private void AddNewRoom(object sender, RoutedEventArgs e)
        {
            AddNewRoomDuringRenovation newRoom = new AddNewRoomDuringRenovation(RoomRenovation);
            NavigationService.Navigate(newRoom);
        }

        private ObservableCollection<String> FindRoomsFromSameFloor()
        {
            RoomsFromSameFloor = new ObservableCollection<String>();

            if (roomCB.SelectedItem != null)
            {
                Room RenovatingRoom = roomStorage.GetOne(roomCB.Text);
                foreach (Room r in roomStorage.GetAll())
                    if (r.Floor == RenovatingRoom.Floor && !r.Id.Equals(RenovatingRoom.Id))
                        RoomsFromSameFloor.Add(r.Id + "-" + r.Name); 
            }
            else
            {
                foreach (Room r in roomStorage.GetAll())
                    RoomsFromSameFloor.Add(r.Id + "-" + r.Name);
            }
            return RoomsFromSameFloor;
        }

        private void cancel(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }

        private void back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Renovations());
        }
    }

}
