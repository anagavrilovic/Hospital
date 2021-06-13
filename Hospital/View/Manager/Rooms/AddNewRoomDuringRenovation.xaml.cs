using Hospital.Model;
using Hospital.Repositories;
using Hospital.Services;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Hospital.View.Manager.Rooms
{
    public partial class AddNewRoomDuringRenovation : Page, INotifyPropertyChanged
    {
        private Room _newRoom;
        public Room NewRoom 
        {
            get { return _newRoom; }
            set { _newRoom = value; OnPropertyChanged("NewRoom"); }
        }
        private RoomRenovation _roomRenovation;
        public RoomRenovation RoomRenovation
        {
            get { return _roomRenovation; }
            set { _roomRenovation = value; OnPropertyChanged("RoomRenovation"); }
        }
        private RoomService roomService;

        public AddNewRoomDuringRenovation(RoomRenovation renovation)
        {
            InitializeComponent();
            this.DataContext = this;

            NewRoom = new Room();
            roomService = new RoomService(new RoomFileRepository(), new AppointmentFileRepository());
            RoomRenovation = renovation;
        }

        private void AcceptAddingButtonClick(object sender, RoutedEventArgs e)
        {
            NewRoom.Type = (RoomType)Enum.Parse(typeof(RoomType), typeCB.Text);

            if (!roomService.IsNewRoomIdUnique(NewRoom.Id))
            {
                Hospital.View.Manager.MessageWindow message = new Hospital.View.Manager.MessageWindow("Vec postoji prostorija sa unetom oznakom!");
                message.Show();
            }
            RoomRenovation.RoomsCreatedDuringRenovation.Add(NewRoom);

            NavigationService.GoBack();
        }

        private void CancelAddingButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
